using SwitchSelect.Service;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models;

[Table("Pedidos")]
public class Pedido
{
    [Key]
    public int Id { get; set; }

    // Dados cliente
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }

    [StringLength(100, ErrorMessage = "Número máximo de caracter 100")]
    public string? Nome { get; set; }
   
    // Relação com o endereço
    public int? EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    //Relação com telefone
    public int TelefoneId {  get; set; }

    public virtual Telefone? Telefone { get; set; }


    //informações pedido     
    [ScaffoldColumn(false)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Total do Pedido")]
    public decimal PedidoTotal { get; set; }

    public  decimal? Desconto { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Itens no Pedido")]
    public int TotalItensPedido { get; set; }

    [Display(Name = "Data de envio")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime PedidoEnviado { get; set; }

    [Display(Name = "Data de entrega")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? PedidoEntregueEm { get; set; }

    [StringLength(20)]
    public string? Status {  get; set; }

    public List<PedidoDetalhe> PedidoItens { get; set; } = new List<PedidoDetalhe>();
    public List<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();

}
