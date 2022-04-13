using FinanceApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Repository.DTOs
{
    public class CardDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CardholderName { get; set; }
        public string ExpDate { get; set; }
        public int CardNumber { get; set; }
        public List<Transaction>? TransactionList { get; set; } = new List<Transaction>();
    }
}
