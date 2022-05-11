using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FinanceApp.Enums;

namespace FinanceApp.Models
{
    public class Bond
    {
        [Required] [Key] public int Id { get; set; }

        [Required] public string BondName { get; set; }

        [Required] public DateTime PurchaseDate { get; set; }

        [Required] public int FaceValue { get; set; }

        [Required] public int PurchaseValue { get; set; }

        [Required] public Currency Currency { get; set; }

        [Required] public DateTime MaturityDate { get; set; }

        [Required] public int Count { get; set; }

        [Required] public double ReturnRate { get; set; }

        [Required] public BondReturnInterval ReturnInterval { get; set; }

        [JsonInclude] public double Yield => FaceValue * (ReturnRate / 100) * Count;

        [JsonIgnore] public ApplicationUser? User { get; set; }
        [JsonIgnore] [ForeignKey("User")] public string? UserId { get; set; }
    }
}