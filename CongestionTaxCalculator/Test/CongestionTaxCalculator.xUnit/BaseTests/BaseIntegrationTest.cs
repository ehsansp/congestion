using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CongestionTaxCalculator.Framework.Calculator;
using CongestionTaxCalculator.Framework.Core.TimeProviders;
using CongestionTaxCalculator.Service;
using CongestionTaxCalculator.xUnit.TimeProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using IVehicleService = CongestionTaxCalculator.Framework.Calculator.IVehicleService;
using VehicleService = CongestionTaxCalculator.Framework.Calculator.VehicleService;

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
                    services.AddSingleton<IVehicleService,VehicleService>();
                    services.AddSingleton<ICalculatorService, CalculatorService>();
                });
            });
        Configuration = Application.Server.Services.GetService<IConfiguration>()!;
    }





}