using CongestionTaxCalculator.xUnit.TimeProviders;

namespace CongestionTaxCalculator.xUnit
{
    public class CalculatorTest
    {
        [Fact]
        public void calculate_tax_on_some_times()
        {
            var configurableDateTimeOffsetProvider = Application.Services.GetService<IConfigurableDateTimeOffsetProvider>()!;
        }
    }
}