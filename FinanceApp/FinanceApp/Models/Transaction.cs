using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public ShopCategory Category { get; set; }
        public string Description { get; set; }
        public int CardId { get; set; }
    }
}
