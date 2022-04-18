using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public int CategoryId { get; set; }
        public string Notice { get; set; }
        public int WalletId { get; set; }
        public int Type { get; set; }
    }
}
