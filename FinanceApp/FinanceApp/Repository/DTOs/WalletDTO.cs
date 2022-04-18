using FinanceApp.Enums;
using FinanceApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Repository.DTOs
{
    public class WalletDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string? AccountNumber { get; set; }
        public int Balance { get; set; }
        public string Location { get; set; }
        public Currency Currency { get; set; }
        public List<Transaction>? TransactionList { get; set; } = new List<Transaction>();
    }
}
