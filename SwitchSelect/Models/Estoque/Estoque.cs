using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models.Estoque;

[Table("Estoque")]
public class Estoque
{
    [Key, ForeignKey("Jogo")]
    public int JogoId { get; set; }

    [Required]
    [Display(Name = "Quantidade")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser um número positivo")]
    public int Quantidade { get; set; }

    // Propriedade de navegação
    public virtual Jogo Jogo { get; set; }
}