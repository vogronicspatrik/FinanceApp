using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Stock
    {
        [Required] [Key] public int Id { get; set; }
        
        [Required] public string Symbol { get; set; } = "";
        
        [Required] public DateTime PurchaseTime { get; set; }
        
        [Required] public Currency CurrencyCode { get; set; } = Currency.HUF;
        
        [Required] public double ValueAtPurchase { get; set; }
        
        [Required] public int Amount { get; set; }

        public double CurrentValue { get; set; }
        
        [JsonIgnore] public ApplicationUser? User { get; set; }
        
        [JsonIgnore] [ForeignKey("User")] public string? UserId { get; set; }
    }
}