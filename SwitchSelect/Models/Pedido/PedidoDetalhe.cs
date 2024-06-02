using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models;

public class PedidoDetalhe
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int JogoId { get; set; }
    public string Restricao { get; set; } //status do produto comprado
    public string? ImagemUrl { get; set; }
    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Display(Name = "Jogo")]
    public string? NomeJogo { get; set; }

    [Display(Name = "Data da compra")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DataCompra { get; set; }

    [Display(Name = "Codigo de venda do produto")]
    public virtual Jogo? Jogo { get; set; }
    public virtual Pedido? Pedido { get; set; }
}