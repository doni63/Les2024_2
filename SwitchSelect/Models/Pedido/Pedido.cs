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

    [NotMapped]
    [StringLength(100, ErrorMessage = "Número máximo de caracter 100")]
    public string? Nome { get; set; }
    //public DateTime DataDeNascimento { get; set; }

    //[StringLength(10)]
    //public string? Genero { get; set; }

    //[StringLength(50)]
    //public string? Email { get; set; }

    //[StringLength(11, MinimumLength = 11, ErrorMessage = "Cpf inválido")]
    //[Display(Name = "CPF")]
    //public string? Cpf { get; set; }

    //[StringLength(9, ErrorMessage = "Número máximo de caracter 9")]
    //public string? RG { get; set; }


    // Relação com o endereço
    public int? EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    //[StringLength(100, ErrorMessage = "Limite 100 caracteres")]
    //public string? Logradouro { get; set; }

    //[StringLength(10, ErrorMessage = "Limite 10 caracteres")]
    //public string? Numero { get; set; }

    //[StringLength(8, ErrorMessage = "Limite 8 caracteres")]
    //public string? CEP { get; set; }

    //[StringLength(100)]
    //public string Bairro { get; set; }

    //[StringLength(100)]
    //public string? Cidade { get; set; }

    //[StringLength(100)]
    //public string? Estado {  get; set; }

    //[StringLength(100)]
    //public string? Pais { get; set; }

    //Relação com telefone
    public int TelefoneId {  get; set; }

    public virtual Telefone? Telefone { get; set; }
    //[StringLength(3)]
    //public string? DDD { get; set; }

    //[StringLength(9)]
    //[Display(Name = "Telefone")]
    //public string? NumeroTelefone { get; set; }

    //relação com cartao
    public int cartaoId { get; set; }
    public virtual Cartao? Cartao { get; set; }

    //[Display(Name = "Numero do cartão")]
    //[StringLength(16)]
    //public string? NumeroCartao { get; set; }

    [NotMapped]
    [StringLength(20)]
    public string? Bandeira { get; set; }

    [NotMapped]
    [Display(Name = "Cartao de final")]
    public string? CartaoQuatroDigito { get; set; }

    //[Display(Name = "Nome do titular")]
    //[StringLength(100)]
    //public string TitularDoCartao { get; set; }

    //[Display(Name = "Cpf do titular")]
    //[StringLength(11, MinimumLength = 11, ErrorMessage = "Cpf inválido")]
    //public string CpfTitularCartao { get; set; }

    //[Range(1, 12, ErrorMessage = "O mês de validade deve estar entre 1 e 12.")]
    //[MesValidade(ErrorMessage = "O mês de validade deve ser maior que o mês atual.")]
    //[NotMapped]
    //public int MesValidade { get; set; }

    //[AnoValidade]
    //[NotMapped]
    //public int AnoValidade { get; set; }

    //[DataType(DataType.Date)]
    //[Display(Name = "Data de Validade")]
    //public DateTime? DataValidade { get; set; }

    //[StringLength(3, MinimumLength = 3, ErrorMessage = "CVV inválido")]
    //public string CVV { get; set; }

    [ScaffoldColumn(false)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Total do Pedido")]
    public decimal PedidoTotal { get; set; }

    public  decimal? Desconto { get; set; }
    public virtual Cupom? Cupom { get; set; }

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

    [StringLength(20)]
    public string? Status {  get; set; }

    public List<PedidoDetalhe>? PedidoItens { get; set; }
}
