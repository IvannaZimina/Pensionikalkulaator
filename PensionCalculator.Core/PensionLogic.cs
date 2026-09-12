using System;

namespace PensionCalculator.Core
{
    public static class PensionLogic
    {
        public static string Calculate(string ageStr, string targetAgeStr, string savingsStr, string monthlyStr, string rateStr)
        {
            if (string.IsNullOrWhiteSpace(ageStr) ||
                string.IsNullOrWhiteSpace(targetAgeStr) ||
                string.IsNullOrWhiteSpace(savingsStr) ||
                string.IsNullOrWhiteSpace(monthlyStr) ||
                string.IsNullOrWhiteSpace(rateStr))
            {
                throw new ArgumentException("Palun täitke kõik väljad.");
            }

            ageStr = ageStr.Replace(',', '.');
            targetAgeStr = targetAgeStr.Replace(',', '.');
            savingsStr = savingsStr.Replace(',', '.');
            monthlyStr = monthlyStr.Replace(',', '.');
            rateStr = rateStr.Replace(',', '.');

            if (!int.TryParse(ageStr, out int currentAge) ||
                !int.TryParse(targetAgeStr, out int targetAge) ||
                !decimal.TryParse(savingsStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal currentSavings) ||
                !decimal.TryParse(monthlyStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal monthlyContribution) ||
                !decimal.TryParse(rateStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal annualRate))
            {
                throw new FormatException("Palun sisestage väljadesse ainult numbrilised väärtused.");
            }

            if (currentAge < 0 || currentAge > 120)
                throw new ArgumentException("Praegune vanus peab olema vahemikus 0 kuni 120 aastat.");

            if (targetAge <= currentAge)
                throw new ArgumentException("Sihtvanus peab olema suurem kui praegune vanus.");

            if (targetAge > 120)
                throw new ArgumentException("Sihtvanus ei saa ületada 120 aastat.");

            if (currentSavings < 0)
                throw new ArgumentException("Praegused säästud ei saa olla negatiivsed.");

            if (monthlyContribution < 0)
                throw new ArgumentException("Iga-kuune makse ei saa olla negatiivne.");

            if (annualRate < 0 || annualRate > 100)
                throw new ArgumentException("Aastane tootlus peab olema vahemikus 0% kuni 100%.");

            int years = targetAge - currentAge;
            int totalMonths = years * 12;
            decimal monthlyRate = (annualRate / 100m) / 12m;

            decimal totalCapital = currentSavings;

            for (int i = 0; i < totalMonths; i++)
            {
                totalCapital = (totalCapital * (1 + monthlyRate)) + monthlyContribution;
            }

            totalCapital = Math.Round(totalCapital, 2);

            return $"{totalCapital:N2} €";
        }
    }
}