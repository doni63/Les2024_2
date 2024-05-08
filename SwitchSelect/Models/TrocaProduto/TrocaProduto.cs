using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models;

public class TrocaProduto
{
    public int Id { get; set; }

    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Required(ErrorMessage = "Por favor, especifique o motivo da troca.")]
    [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
    public string Motivo { get; set; }

    public int Qtd { get; set; }//quantidade de produtos para trocar
    public int JogoId { get; set; }
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "Informe o nome do jogo.")]
    [StringLength(100,ErrorMessage = "Máximo 100 caracteres.")]
    [Display(Name = "Nome do Jogo")]
    public string NomeJogo { get; set; }
    public int PedidoId { get; set; } // Chave estrangeira para associar a troca ao pedido original
    public Pedido? Pedido { get; set; } // Referência de navegação para o pedido original

    // Campos para controle de status da solicitação de troca, como "Em andamento", "Concluída", etc.
    public string? Status { get; set; }
    public DateTime DataSolicitacao { get; set; }
}
