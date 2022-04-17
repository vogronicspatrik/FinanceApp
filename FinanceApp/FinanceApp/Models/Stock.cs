namespace FinanceApp.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Symbol { get; set; } = "";
        public DateTime PurchaseTime { get; set; }
        public string CurrencyCode { get; set; } = "HUF";
        public double ValueAtPurchase { get; set; }
        public int Amount { get; set; }
    }
}