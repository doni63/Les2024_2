using SwitchSelect.Service;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models.ViewModels;
 
public class ClientePedidoViewModel
{
    //Informação de cliente
    public int ClienteId { get; set; }
    public int EnderecoId { get; set; }
    public int TelefoneId {  get; set; }
    public int CartaoId {  get; set; }

    //dados de pedido
    public string? Status { get; set; }
    public Pedido? Pedido { get; set; }
}
