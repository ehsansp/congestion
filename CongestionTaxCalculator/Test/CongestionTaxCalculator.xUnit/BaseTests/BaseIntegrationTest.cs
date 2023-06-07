using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CongestionTaxCalculator.Framework.Calculator;
using CongestionTaxCalculator.Framework.Core.TimeProviders;
using CongestionTaxCalculator.xUnit.TimeProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.xUnit.BaseTests;

public class BaseIntegrationTest
{
    protected readonly WebApplicationFactory<Program> Application;
    protected readonly IConfiguration Configuration;

    protected BaseIntegrationTest()
    {
        Application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment(EnvironmentNames.Test);

                builder.UseTestServer(testServerOptions => testServerOptions.PreserveExecutionContext = true);

                builder.ConfigureServices(services =>
                {
                    var dateTimeOffsetProvider = services.SingleOrDefault(d => d.ServiceType == typeof(IDateTimeOffsetProvider));
                    if (dateTimeOffsetProvider != null)
                    {
                        services.Remove(dateTimeOffsetProvider);
                    }
                    var testDateTimeOffsetProvider = new TestDateTimeOffsetProvider();
                    services.AddSingleton<IDateTimeOffsetProvider>(testDateTimeOffsetProvider);
                    services.AddSingleton<IConfigurableDateTimeOffsetProvider>(testDateTimeOffsetProvider);
                });
            });
        Configuration = Application.Server.Services.GetService<IConfiguration>()!;
    }





}