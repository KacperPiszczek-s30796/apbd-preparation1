using System.ComponentModel.DataAnnotations;

namespace WebApplication4.contracts.requests;

public class CreateProductWarehouse
{
    [Required] [Range(1, int.MaxValue)] public int IdProduct { get; set; }
    [Required] [Range(1, int.MaxValue)] public int IdWarehouse { get; set; }
    [Required] [Range(1, int.MaxValue)] public int Amount { get; set; }
    [Required] [StringLength(120)] public string CreatedAt { get; set; } = string.Empty;
}