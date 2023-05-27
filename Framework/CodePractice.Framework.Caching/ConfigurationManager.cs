using Microsoft.Extensions.Configuration;

namespace CodePractice.Framework.Caching;

public static class ConfigurationManager
{
    public static IConfiguration AppSetting
    {
        get;
        set;
    }
}