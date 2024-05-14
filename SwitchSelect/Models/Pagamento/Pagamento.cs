using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models;

public class Pagamento
{
    public int Id { get; set; }
    public int? PedidoId { get; set; }
    public decimal Valor { get; set; }

    [StringLength(20)]
    public string? Tipo { get; set; }

    public List<int>? CartaoIds { get; set; } = new List<int>();


    public List<string>? CodigosCupons { get; set; } = new List<string>();

    [StringLength(14)]
    public string? CodigoCupom { get; set; }

    [Display(Name = "Satatus")]
    public string? StatusPagamento { get; set; }
}
