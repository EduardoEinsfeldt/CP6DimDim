using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DimDimApi.Models;

public class Conta
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [Required]
    [MaxLength(20)]
    public string NumeroConta { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Saldo { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public Cliente? Cliente { get; set; }
}
