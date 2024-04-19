using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models;

[Table("Pedidos")]
public class Pedido
{
    [Key]
    public int Id { get; set; }

    // Relação com o cliente
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    // Relação com o endereço
    public int EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }

    //Relação com telefone
    public int TelefoneId {  get; set; }
    public Telefone? Telefone { get; set; }

    //relação com cartao
    public int cartaoId { get; set; }
    public Cartao? Cartao { get; set; }

    [ScaffoldColumn(false)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Total do Pedido")]
    public decimal PedidoTotal { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Itens no Pedido")]
    public int TotalItensPedido { get; set; }

    [Display(Name = "Data do Pedido")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime PedidoEnviado { get; set; }

    [Display(Name = "Data Envio Pedido")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? PedidoEntregueEm { get; set; }

    public string? Status {  get; set; }
    public List<PedidoDetalhe> PedidoItens { get; set; }
}
