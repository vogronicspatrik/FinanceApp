using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class Category
    {
        [Required] [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Color { get; set; }

        [JsonIgnore] public ApplicationUser? User { get; set; }
        [JsonIgnore] [ForeignKey("User")] public string? UserId { get; set; }
    }
}