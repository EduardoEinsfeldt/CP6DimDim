using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DimDimApi.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public ICollection<Conta> Contas { get; set; } = new List<Conta>();
}
