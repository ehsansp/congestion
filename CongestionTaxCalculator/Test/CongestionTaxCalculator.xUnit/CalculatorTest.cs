using CongestionTaxCalculator.Framework.Calculator;
using CongestionTaxCalculator.Service;
using CongestionTaxCalculator.xUnit.BaseTests;
using CongestionTaxCalculator.xUnit.TimeProviders;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.xUnit
{
    public class CalculatorTest : BaseIntegrationTest
    {
        private ICalculatorService _calculatorService;

        protected CalculatorTest(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }
        [Fact]
        public void calculate_tax_on_some_times()
        {
            var configurableDateTimeOffsetProvider = Application.Services.GetService<IConfigurableDateTimeOffsetProvider>()!;
            DateTime[] dates;
            var changeableDateTime = new DateTimeOffset(2013, 1, 14, 21, 0, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 1, 15, 21, 0, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 7, 6, 23, 27, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 7, 15, 27, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 6, 27, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 6, 20, 27, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 14, 35, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 15, 29, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 15, 47, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 16, 1, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 16, 48, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 17, 49, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 18, 29, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 2, 8, 18, 35, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 3, 26, 14, 25, 0, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            changeableDateTime = new DateTimeOffset(2013, 3, 28, 14, 7, 27, TimeSpan.Zero);
            configurableDateTimeOffsetProvider.SetUtcNow(changeableDateTime);
            dates = new[] { Convert.ToDateTime(changeableDateTime) };

            var result = _calculatorService.GetTax(TollFreeVehicles.Diplomat, dates);
            result.Should().Be(0);

            result = _calculatorService.GetTax(TollFreeVehicles.Tractor, dates);
            result.Should().Be(156);

            result = _calculatorService.GetTax(TollFreeVehicles.Emergency, dates);
            result.Should().Be(0);

            result = _calculatorService.GetTax(TollFreeVehicles.Bus, dates);
            result.Should().Be(0);

            result = _calculatorService.GetTax(TollFreeVehicles.Foreign, dates);
            result.Should().Be(0);

            result = _calculatorService.GetTax(TollFreeVehicles.Military, dates);
            result.Should().Be(0);
        }
    }
}