using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FinanceApp.Enums;
using FinanceApp.Other;

namespace FinanceApp.Models;

public class Loan
{
    [Required] [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string OtherParty { get; set; }

    [Required] public double Amount { get; set; }

    [Required] public Currency Currency { get; set; }

    [Required] public int TermInMonths { get; set; }

    [Required] public double InterestRate { get; set; }

    public int RemainingMonths { get; set; }

    public double RemainingAmount { get; set; }

    [JsonInclude]
    [NotMapped]
    public double MonthlyInstallment =>
        Math.Round(HelperMethods.CalculateLoanAmortizationAmount(RemainingAmount, InterestRate / 100, RemainingMonths));

    [JsonInclude]
    [NotMapped]
    public List<LoanAmortizationScheduleEntry> LoanSchedule =>
        HelperMethods.CalculateLoanSchedule(RemainingAmount, InterestRate / 100, RemainingMonths, 10);

    [JsonIgnore] public ApplicationUser? User { get; set; }
    [JsonIgnore] [ForeignKey("User")] public string? UserId { get; set; }
}