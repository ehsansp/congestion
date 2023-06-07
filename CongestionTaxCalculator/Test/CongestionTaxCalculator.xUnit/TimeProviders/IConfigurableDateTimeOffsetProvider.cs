using CongestionTaxCalculator.Framework.Core.TimeProviders;

namespace CongestionTaxCalculator.xUnit.TimeProviders;

public interface IConfigurableDateTimeOffsetProvider: IDateTimeOffsetProvider
{
    void ResetUtcNow();
    void SetUtcNow(DateTimeOffset dateTimeOffset);
}