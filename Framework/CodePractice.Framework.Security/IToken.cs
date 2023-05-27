using CodePractice.Framework.Caching;
using CodePractice.Framework.Dto;
using HashidsNet;

namespace CodePractice.Framework.Security;

public interface IToken
{
    TokenModel GetToken(string token);
    bool ChechToken(string token);

    TokenModelViewModel addToken(User user);
    TokenModelViewModel addUserToken(long userId, string userName);



    TokenModel addToken(long userId, string UserName, string token, List<string> Roles = null);
    void deleteToken(long userId);
    Task<bool> deleteUserToken(long userId);
    void updateToken(long userId);
    Task Expire(string token);
    long? getUser(string token);
    TokenModelViewModel? getUserModel(string token);
    List<TokenModel> getAll();
    bool removeLoginUserByToken(string token);
}
public class Token : IToken
{
    private readonly ICacheService _cacheService;
    public Token(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public TokenModel GetToken(string token)
    {
        var t = new TokenModel();
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens != null)
        {
            //t = _tokens.FirstOrDefault(x => x.Token == token);
        }
        return t;
    }

    public bool ChechToken(string token)
    {
        var t = false;
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens != null)
        {
            //t = _tokens.Any(x => x.Token == token);
        }
        return t;
    }

    private string userIdHashSalt = "pafjo;wiejr24o5uPO(UOKfwjpo3i8j4";
    public TokenModelViewModel addToken(User user)
    {
        if (user.Id == null || user.Id <= 0)
        {
            throw new Exception("خطا در پردازش توکن");
        }

        //if (user.Tokens is { Count: > 9 })
        //{
        //    throw new Exception("محدودیت تعداد لاگین");
        //}
        // encoding user id
        Hashids userIdHasher = new Hashids(userIdHashSalt, minHashLength: 10);
        var userIdHashed = userIdHasher.EncodeLong(user.Id);
        var currentSessions = _cacheService.GetData<TokenModel>($"USR:{userIdHashed}");
        if (currentSessions != null && currentSessions.Tokens != null && currentSessions.Tokens.Any())
        {
            if (user.Roles != currentSessions.Roles)
            {
                currentSessions.Roles = user.Roles;
            }
            if (currentSessions.Tokens.Count > 9)
            {
                //TODO : some how notify user that is has login limit
                var res = new TokenModelViewModel()
                {
                    Created = currentSessions.Created,
                    LastUse = DateTime.Now,
                    UserID = currentSessions.UserID,
                    Roles = currentSessions.Roles,
                    Token = currentSessions.Tokens.Last().Token,
                    UserName = currentSessions.UserName
                };
                return res;
            }
        }

        // creating token
        var salt = Guid.NewGuid().ToString("N");
        Hashids hs = new Hashids(salt, minHashLength: 100);
        var ticks = DateTime.Now.Ticks;
        var hash = hs.EncodeLong(ticks);
        // client id => maybe later on i can join client id and user id into an object
        var clientId = Guid.NewGuid().ToString("N");

        var ct = new ClientToken()
        {
            ClientId = clientId,
            Token = $"{userIdHashed}_{clientId}_{hash}"
        };
        if (currentSessions != null && currentSessions.Tokens != null && currentSessions.Tokens.Count < 10)
        {
            if (user.Roles != currentSessions.Roles)
            {
                currentSessions.Roles = user.Roles;
            }
            currentSessions.Tokens.Add(ct);
            _cacheService.SetData($"USR:{userIdHashed}", currentSessions, DateTimeOffset.Now.AddYears(1));
            var res = new TokenModelViewModel()
            {
                Created = currentSessions.Created,
                LastUse = DateTime.Now,
                UserID = currentSessions.UserID,
                Roles = currentSessions.Roles,
                Token = ct.Token,
                UserName = currentSessions.UserName
            };
            return res;
        }
        else
        {
            //if (currentSessions.Tokens == null )
            //{
            //    currentSessions.Tokens = new List<ClientToken>();
            //}
            var tk = new TokenModelViewModel()
            {
                Created = DateTime.Now,
                Token = ct.Token,
                UserID = user.Id,
                Roles = user.Roles,
                LastUse = DateTime.Now,
                UserName = user.UserName
            };
            _cacheService.SetData($"USR:{userIdHashed}", tk, DateTimeOffset.Now.AddYears(1));
            return tk;
        }
    }

    public TokenModel addToken(long userId, string UserName, string token, List<string> Roles = null)
    {
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens == null)
            _tokens = new List<TokenModel>();
        var tt = _tokens.Where(x => x.UserID == userId).ToList();
        if (tt.Count > 0)
            foreach (var i in tt)
                _tokens.Remove(i);
        var t = new TokenModel() { Created = DateTime.UtcNow, UserID = userId, UserName = UserName, /*Token = token,*/ LastUse = DateTime.UtcNow, Roles = Roles };

        _tokens.Add(t);
        _cacheService.SetData("tokens", _tokens, DateTimeOffset.Now.AddDays(365));
        return t;
    }
    public void deleteToken(long userId)
    {
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens == null)
            _tokens = new List<TokenModel>();
        var tt = _tokens.RemoveAll(x => x.UserID == userId);
        //if (tt.Count > 0)
        //    foreach (var i in tt)
        //        _tokens.Remove(i);
    }

    public void updateToken(long userId)
    {
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens == null)
            _tokens = new List<TokenModel>();
        var tt = _tokens.Where(x => x.UserID == userId).FirstOrDefault();
        if (tt != null)
            tt.LastUse = DateTime.UtcNow;
    }
    public void updateToken(string token)
    {
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens == null)
            _tokens = new List<TokenModel>();
        var tt = _tokens.Where(x => true /*x.Token == token*/).FirstOrDefault();
        if (tt != null)
            tt.LastUse = DateTime.UtcNow;
    }
    public async Task Expire(string token)
    {
        var data = token.Split('_');
        Hashids userIdHasher = new Hashids(userIdHashSalt, minHashLength: 10);
        var userIdHashed = userIdHasher.DecodeLong(data[0]).First();
        var clientId = data[1];


        var _tokens = _cacheService.GetData<TokenModel>($"USR:{data[0]}");
        if (_tokens == null)
            return;
        if (_tokens.Tokens == null || _tokens.Tokens.Count == 0)
        {
            return;
        }

        var find = _tokens.Tokens.FirstOrDefault(x => x.Token == token);
        if (find == null)
        {
            return;
        }
        else
        {
            _tokens.Tokens.Remove(find);
        }
        await _cacheService.SetDataAsync($"USR:{data[0]}", _tokens, DateTimeOffset.Now.AddYears(1));
    }
    public long? getUser(string token)
    {
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens == null)
            _tokens = new List<TokenModel>();
        var tt = _tokens.Where(x => true/*x.Token == token*/).FirstOrDefault();
        if (tt != null)
        {
            tt.LastUse = DateTime.UtcNow;
            return tt.UserID;
        }
        else
            return null;
    }
    public TokenModelViewModel? getUserModel(string token)
    {
        var data = token.Split('_');
        Hashids userIdHasher = new Hashids(userIdHashSalt, minHashLength: 10);
        var userIdHashed = userIdHasher.DecodeLong(data[0]).First();
        var clientId = data[1];


        var _tokens = _cacheService.GetData<TokenModel>($"USR:{data[0]}");
        if (_tokens == null)
            return null;
        if (_tokens.Tokens == null || _tokens.Tokens.Count == 0)
        {
            return null;
        }

        var find = _tokens.Tokens.FirstOrDefault(x => x.Token == token);
        if (find == null)
        {
            return null;
        }
        _cacheService.SetData($"USR:{data[0]}", _tokens, DateTimeOffset.Now.AddYears(1));
        var res = new TokenModelViewModel()
        {
            Created = _tokens.Created,
            LastUse = DateTime.Now,
            UserID = userIdHashed,
            Roles = _tokens.Roles,
            Token = find.Token,
            UserName = _tokens.UserName
        };
        return res;
    }

    public List<TokenModel> getAll()
    {
        var _tokens = _cacheService.GetData<List<TokenModel>>("tokens");
        if (_tokens == null)
            _tokens = new List<TokenModel>();
        var tt = _tokens.ToList();
        return tt;
    }

    public TokenModelViewModel addUserToken(long userId, string userName)
    {
        TokenModelViewModel token = new TokenModelViewModel();
        Hashids userIdHasher = new Hashids(userIdHashSalt, minHashLength: 10);
        var userIdHashed = userIdHasher.EncodeLong(userId);
        var currentSessions = _cacheService.GetData<TokenModel>($"USRSignIn:{userIdHashed}");
        if (currentSessions != null && currentSessions.Tokens != null && currentSessions.Tokens.Any())
        {
            //TODO : some how notify user that is has login limit
            token = new TokenModelViewModel()
            {
                Created = currentSessions.Created,
                LastUse = DateTime.Now,
                UserID = currentSessions.UserID,
                Roles = currentSessions.Roles,
                Token = currentSessions.Tokens.Last().Token,
                UserName = currentSessions.UserName
            };
            return token;
        }

        // creating token
        var salt = Guid.NewGuid().ToString("N");
        Hashids hs = new Hashids(salt, minHashLength: 100);
        var ticks = DateTime.Now.Ticks;
        var hash = hs.EncodeLong(ticks);
        // client id => maybe later on i can join client id and user id into an object
        var clientId = Guid.NewGuid().ToString("N");

        var ct = new ClientToken()
        {
            ClientId = clientId,
            Token = $"{userIdHashed}_{clientId}_{hash}"
        };
        token = new TokenModelViewModel()
        {
            Created = DateTime.Now,
            Token = ct.Token,
            UserID = userId,
            Roles = null,
            LastUse = DateTime.Now,
            UserName = userName
        };
        var cacheSuccess = _cacheService.SetData($"USRSignIn:{userIdHashed}", token, DateTimeOffset.Now.AddYears(1));
        if (!cacheSuccess)
        {
            return token = null;
        }
        return token;

    }

    public async Task<bool> deleteUserToken(long userId)
    {
        Hashids userIdHasher = new Hashids(userIdHashSalt, minHashLength: 10);
        var userIdHashed = userIdHasher.EncodeLong(userId);
        var result = await _cacheService.RemoveDataAsync($"USRSignIn:{userIdHashed}");
        if (result)
        {
            return true;
        }
        return false;
    }

    public bool removeLoginUserByToken(string token)
    {
        var result = _cacheService.RemoveData($"USRSignIn:{token}");
        if (!result)
        {
            return false;
        }
        return true;
    }
}
