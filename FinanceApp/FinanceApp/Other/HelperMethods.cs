using FinanceApp.AlphaVantageModels;
using FinanceApp.Models;
using Newtonsoft.Json;

namespace FinanceApp.Other;

public static class HelperMethods
{
    public static int CalculateYearDifference(DateTime d1, DateTime d2)
    {
        var a = (d1.Year * 100 + d1.Month) * 100 + d1.Day;
        var b = (d2.Year * 100 + d2.Month) * 100 + d2.Day;

        return Math.Abs(a - b) / 10000;
    }

    public static double CalculateLoanAmortizationAmount(double totalBalance, double interestRate, int periodInMonth)
    {
        var x = Math.Pow(1 + interestRate / 12, periodInMonth);
        var amount = totalBalance * (interestRate / 12 * x) / (x - 1);
        return double.IsNaN(amount) ? 0 : amount;
    }

    public static List<LoanAmortizationScheduleEntry> CalculateLoanSchedule(double totalBalance, double interestRate,
        int periodInMonth, int iterations)
    {
        var schedule = new List<LoanAmortizationScheduleEntry>();
        var amortizationAmount = CalculateLoanAmortizationAmount(totalBalance, interestRate, periodInMonth);
        var entryIndex = 1;
        var startBalance = totalBalance;
        while (entryIndex <= periodInMonth && entryIndex <= iterations)
        {
            var interest = startBalance * (interestRate / 12);
            var principal = amortizationAmount - interest;
            var endBalance = Math.Max(startBalance - principal, 0);
            schedule.Add(new LoanAmortizationScheduleEntry(entryIndex, Math.Round(startBalance),
                Math.Round(interest), Math.Round(principal),
                Math.Round(endBalance)));
            startBalance = endBalance;
            entryIndex += 1;
        }

        return schedule;
    }

    public static async Task<RealTimeFxItem> FxRealTime(string fromSymbol, string toSymbol)
    {
        const string APIKEY = "BRJ4GQ9MKH1K08JX";
        var _httpClient = new HttpClient();

        var queryString =
            $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={fromSymbol}&to_currency={toSymbol}&apikey={APIKEY}";
        var uri = new Uri(queryString);

        var response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode) return null;
        var jsonStr = await response.Content.ReadAsStringAsync();
        if (jsonStr == null) return null;
        
        var fxData = JsonConvert.DeserializeObject<RealTimeFxData>(jsonStr);
        return fxData.FxItem;
    }
}