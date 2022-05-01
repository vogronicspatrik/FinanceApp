using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Bond
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string BondName { get; set; }
        public DateTime PurchaseTime { get; set; }
        public double BasePrice { get; set; }
        public int Count { get; set; }
        public double ReturnRate { get; set; }
        public BondReturnInterval ReturnInterval { get; set; }
    }
}