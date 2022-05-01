using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Wallet
    {
        [Required] [Key] public int Id { get; set; }

        [Required] public string Type { get; set; }

        [Required] public string Location { get; set; }

        public string? Owner { get; set; }

        public string? AccountNumber { get; set; }

        [DefaultValue(0)] public int Balance { get; set; }

        public Currency Currency { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }

        [JsonIgnore] public ApplicationUser? User { get; set; }
        [JsonIgnore] [ForeignKey("User")] public string? UserId { get; set; }
    }
}