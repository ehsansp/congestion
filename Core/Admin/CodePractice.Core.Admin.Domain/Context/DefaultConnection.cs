using Microsoft.Extensions.Configuration;

namespace CodePractice.Core.Admin.Domain.Context;

public class DefaultConnection
{
    public static string ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MSAdminDB;Data Source=.;Encrypt=false;";
    public static void Set(IConfiguration config)
    {
        ConnectionString = config.GetConnectionString(nameof(DefaultConnection));
    }
}