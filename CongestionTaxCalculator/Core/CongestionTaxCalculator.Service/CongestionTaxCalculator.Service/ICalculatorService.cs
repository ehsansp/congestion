using CongestionTaxCalculator.Framework.Calculator;

namespace CongestionTaxCalculator.Service;

public interface ICalculatorService
{
    int GetTax(TollFreeVehicles vehicle, DateTime[] dates);
    bool IsTollFreeVehicle(TollFreeVehicles vehicle);
    int GetTollFee(DateTime date, TollFreeVehicles vehicle);
    Boolean IsTollFreeDate(DateTime date);

}