namespace CodePractice.Framework.Dto;

public class TokenModel
{
    public long UserID { set; get; }
    public List<ClientToken> Tokens { set; get; } = new List<ClientToken>();
    public string UserName { set; get; }
    public DateTime? Created { set; get; }
    public DateTime? LastUse { set; get; }
    public List<string> Roles { set; get; }
}

public class TokenModelViewModel
{
    public long UserID { set; get; }
    public string Token { set; get; }
    public string UserName { set; get; }
    public DateTime? Created { set; get; }
    public DateTime? LastUse { set; get; }
    public List<string> Roles { set; get; }
}
