using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models;

public class Devolucao
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Obrigatório preencher o motivo da devolução")]
    [StringLength(200, ErrorMessage = "Maximo de caracteres 200")]
    [MinLength(8, ErrorMessage = "Explique melhor o motivo")]
    public string Motivo { get; set; }
    public int PedidoId { get; set; }
    public int ClienteId { get; set; }
    public string NomeCliente { get; set; }
    public string CpfCliente { get; set; }
    public string StatusDevolucao { get; set; }
    public DateTime? DataSolicitacao { get; set; }
    public DateTime? DataConfirmacao { get; set; }
}
