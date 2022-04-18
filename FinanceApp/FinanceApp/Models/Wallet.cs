using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string? AccountNumber { get; set; }
        public int Balance { get; set; }
        public string Location { get; set; }
        public Currency Currency { get; set; }
    }
}
