namespace FinanceApp.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardholderName { get; set; }
        public string ExpDate { get; set; }
        public int CardNumber { get; set; }
        public List<Transaction> TransactionList { get; set; }
    }
}
