using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models;

public class Devolucao
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Required(ErrorMessage = "Por favor, especifique o motivo da troca.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Motivo { get; set; }
    public decimal Valor { get; set; }
    public int PedidoId { get; set; } // Chave estrangeira para associar a troca ao pedido original
    public Pedido? Pedido { get; set; } // Referência de navegação para o pedido original

    // Campos para controle de status da solicitação de devolucao
    public string? Status { get; set; }
    public DateTime DataSolicitacao { get; set; }
}
