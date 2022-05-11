using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceApp.Enums;
using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class Transaction
    {
        [Required] [Key] public int Id { get; set; }

        [Required] public string Notice { get; set; }

        [Required] public TransactionType Type { get; set; }

        [Required] public int Price { get; set; }

        [Required] public DateTime DateOfTransaction { get; set; }

        [DefaultValue(false)] public bool IsCategorized { get; set; }

        public virtual Category? Category { get; set; }
        [ForeignKey("Category")] public int? CategoryId { get; set; }

        [JsonIgnore] public Wallet? Wallet { get; set; }
        [ForeignKey("Wallet")] public int WalletId { get; set; }
    }
}