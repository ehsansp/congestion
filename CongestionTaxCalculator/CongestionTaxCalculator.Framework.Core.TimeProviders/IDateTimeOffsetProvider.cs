namespace CongestionTaxCalculator.Framework.Core.TimeProviders
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset UtcNow { get; }
    }
}