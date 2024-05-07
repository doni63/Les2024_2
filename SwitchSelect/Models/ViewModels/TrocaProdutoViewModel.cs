using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models.ViewModels;

public class TrocaProdutoViewModel
{
    public int JogoId { get; set; }

    public int PedidoId { get; set; }

    [Required(ErrorMessage = "Informe o nome do jogo.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    [Display(Name = "Nome do Jogo")]
    public string? NomeJogo { get; set; }

    [Required(ErrorMessage = "Por favor, especifique o motivo da troca.")]
    [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
    public string? Motivo { get; set; }
    public List<int> ProdutosIds { get; set; } = new List<int>();
}
