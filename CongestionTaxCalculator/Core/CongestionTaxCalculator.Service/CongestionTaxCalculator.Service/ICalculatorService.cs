using CongestionTaxCalculator.Framework.Calculator;

namespace CongestionTaxCalculator.Service;

public interface ICalculatorService
{
    int GetTax(Vehicle vehicle, DateTime[] dates);
    bool IsTollFreeVehicle(Vehicle vehicle);
    int GetTollFee(DateTime date, Vehicle vehicle);
    Boolean IsTollFreeDate(DateTime date);

}