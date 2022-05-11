using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FinanceApp.Enums;

namespace FinanceApp.Models;

public class CashFlowItem
{
    [Required] [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string OtherParty { get; set; }

    [Required] public int Amount { get; set; }

    [Required] public Currency Currency { get; set; }

    [Required] public CashFlowDirectionType Type { get; set; }
    
    [Required] public RecurrencyPeriod RecurrencyPeriod { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }
    [JsonIgnore] [ForeignKey("User")] public string? UserId { get; set; }
}