using CongestionTaxCalculator.Framework.Calculator;

namespace CongestionTaxCalculator.Service;

public class VehicleService: IVehicleService
{
    public bool GetVehicleType(TollFreeVehicles vehicles)
    {
        return vehicles.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicles.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicles.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicles.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicles.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicles.Equals(TollFreeVehicles.Military.ToString());
    }
}