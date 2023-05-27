using Newtonsoft.Json;
using StackExchange.Redis;

namespace CodePractice.Framework.Caching;

public class CacheService : ICacheService
{
    private IDatabase _db;
    public CacheService()
    {
        ConfigureRedis();
    }
    private void ConfigureRedis()
    {
        _db = ConnectionHelper.Connection.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = _db.StringGet(key);

        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        return isSet;
    }

    public bool RemoveData(string key)
    {
        bool _isKeyExist = _db.KeyExists(key);
        if (_isKeyExist == true)
        {
            return _db.KeyDelete(key);
        }

        return false;
    }

    public async Task<T> GetDataAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);

        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }

    public async Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);

        var isSet = await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);

        return isSet;
    }

    public async Task<bool> RemoveDataAsync(string key)
    {
        bool _isKeyExist = await _db.KeyExistsAsync(key);

        if (_isKeyExist == true)
        {
            return _db.KeyDelete(key);
        }

        return false;
    }
}
