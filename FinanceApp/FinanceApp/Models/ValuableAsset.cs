using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FinanceApp.Enums;
using FinanceApp.Other;

namespace FinanceApp.Models;

public class ValuableAsset
{
    [Required] [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public DateTime DateOfPurchasing { get; set; }

    [Required] public int OriginalValue { get; set; }

    [Required] public Currency Currency { get; set; }

    [Required] public int AmortizationRatePerYear { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }
    [JsonIgnore] [ForeignKey("User")] public string? UserId { get; set; }

    [JsonInclude]
    public int CurrentValue => Math.Max(OriginalValue - Convert.ToInt32(OriginalValue *
                                                                        HelperMethods.CalculateYearDifference(
                                                                            DateTime.Now, DateOfPurchasing) *
                                                                        (AmortizationRatePerYear / 100.0)), 0);
}