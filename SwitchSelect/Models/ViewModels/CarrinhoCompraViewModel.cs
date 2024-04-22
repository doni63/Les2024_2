using SwitchSelect.Models;
using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models.ViewModels;

public class CarrinhoCompraViewModel
{
    public CarrinhoCompra CarrinhoCompra { get; set; }
    public decimal CarrinhoCompraTotal { get; set; }
    public int QuantidadeProdutosTotal { get; set; }

   [Display(Name = "Código do Cupom")]
    public string CodigoCupom { get; set; }
}
