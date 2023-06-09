﻿using CongestionTaxCalculator.Framework.Calculator;

namespace CongestionTaxCalculator.Service;

public class CalculatorService:ICalculatorService
{
    public int GetTax(TollFreeVehicles vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public bool IsTollFreeVehicle(TollFreeVehicles vehicle)
    {
        if(vehicle != TollFreeVehicles.Tractor) return false;
        return true;
    }

    public int GetTollFee(DateTime date, TollFreeVehicles vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        return 0;
    }

    public Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }
}