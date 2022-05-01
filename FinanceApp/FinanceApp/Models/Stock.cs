using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Symbol { get; set; } = "";
        public DateTime PurchaseTime { get; set; }
        public Currency CurrencyCode { get; set; } = Currency.HUF;
        public double ValueAtPurchase { get; set; }
        public int Amount { get; set; }
    }
}