using System;
using StackExchange.Redis;

namespace CodePractice.Framework.Caching;

public class ConnectionHelper
{
    static ConnectionHelper()
    {
        ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]));
    }
    private static readonly Lazy<ConnectionMultiplexer> lazyConnection;
    public static ConnectionMultiplexer Connection => lazyConnection.Value;

    public static IServer Server => lazyConnection.Value.GetServer(ConfigurationManager.AppSetting["RedisURL"]);
}