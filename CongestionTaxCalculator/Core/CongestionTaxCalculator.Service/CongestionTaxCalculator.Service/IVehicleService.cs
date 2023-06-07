using CongestionTaxCalculator.Framework.Calculator;

namespace CongestionTaxCalculator.Service;

public interface IVehicleService
{
    bool GetVehicleType(TollFreeVehicles vehicles);
}