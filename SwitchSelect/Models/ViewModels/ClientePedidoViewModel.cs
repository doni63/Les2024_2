using SwitchSelect.Service;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models.ViewModels;
 
public class ClientePedidoViewModel
{
    //Informação de cliente
    
    [Required(ErrorMessage = "Informe o nome")]
    [StringLength(100, ErrorMessage = "Número máximo de caracter 100")]
    public string Nome { get; set; }

    [Display(Name = "Data de Nascimento")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Informe a data de nascimento")]
    public DateTime DataDeNascimento { get; set; }

    [Required(ErrorMessage = "Informe o genero")]
    [StringLength(10)]
    public string Genero { get; set; }

    [Required(ErrorMessage = "Informe o email.")]
    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe o Cpf")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Cpf inválido")]
    [Display(Name = "CPF")]
    public string Cpf { get; set; }
    [Required(ErrorMessage = "Informe o RG")]
    [StringLength(9, ErrorMessage = "Número máximo de caracter 9")]
    public string RG { get; set; }

    //informação d telefone

    public TipoTelefone TipoTelefone { get; set; }
    [Required(ErrorMessage = "Informe DDD")]
    [StringLength(3)]
    public string DDD { get; set; }

    [Required(ErrorMessage = "Informe Telefone")]
    [StringLength(9)]
    [Display(Name = "Telefone")]
    public string NumeroTelefone { get; set; }

    //informação Endereco
    [Required(ErrorMessage = "Informe tipo de endereço")]

    [Display(Name = "Tipo de endereço")]
    public TipoEndereco TipoEndereco { get; set; }

    [Required(ErrorMessage = "Informe o tipo de residência")]
    [Display(Name = "Tipo de residência")]
    public TipoResidencia TipoResidencia { get; set; }

    [Required(ErrorMessage = "Informe o tipo de logradouro")]
    [Display(Name = "Tipo de logradoutro")]
    public TipoLogradouro TipoLogradouro { get; set; }


    [Required(ErrorMessage = "Informe o logradouro")]
    [StringLength(100, ErrorMessage = "Limite 100 caracteres")]
    public string Logradouro { get; set; }

    [Display(Name = "Número")]
    [Required(ErrorMessage = "Informe o número")]
    [StringLength(10, ErrorMessage = "Limite 10 caracteres")]
    public string Numero { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "Limite 8 caracteres")]
    public string CEP { get; set; }

    public string? Complemento { get; set; }

    [Required(ErrorMessage = "Informe o Bairro")]
    [StringLength(100)]
    [Display(Name = "Bairro")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "Informe o Bairro")]
    [StringLength(100)]
    [Display(Name = "Cidade")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "Informe o Bairro")]
    [StringLength(100)]
    [Display(Name = "Estado")]
    public string Estado { get; set; }

    //informações de cartão
    [Required(ErrorMessage = "Informe número do cartão")]
    [Display(Name = "Numero do cartão")]
    [StringLength(16)]
    public string NumeroCartao { get; set; }

    [NotMapped]
    [Display(Name = "Cartao de final")]
    public string? CartaoQuatroDigito { get; set; }

    [Required(ErrorMessage = "Informe nome do titular do cartão")]
    [Display(Name = "Nome do titular")]
    [StringLength(100)]
    public string TitularDoCartao { get; set; }

    [Required(ErrorMessage = "Informe cpf do titular")]
    [Display(Name = "Cpf do titular")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Cpf inválido")]
    public string CpfTitularCartao { get; set; }


    [Required(ErrorMessage = "Informe mês de validade do cartão")]
    [Range(1, 12, ErrorMessage = "O mês de validade deve estar entre 1 e 12.")]
    [MesValidade(ErrorMessage = "O mês de validade deve ser maior que o mês atual.")]
    [NotMapped]
    public int MesValidade { get; set; }

    [Required(ErrorMessage = "O ano de validade é obrigatório.")]
    [AnoValidade]
    [NotMapped]
    public int AnoValidade { get; set; }


    [DataType(DataType.Date)]
    [Display(Name = "Data de Validade")]
    public DateTime? DataValidade { get; set; }

    [Required(ErrorMessage = "Informe o CVV")]
    [Display(Name = "CVV")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV inválido")]
    public string CVV { get; set; }

    [Required]
    [Display(Name = "Tipo de cartão")]
    public TipoCartao TipoCartao { get; set; }

    //dados de pedido
    public string? Status { get; set; }
    public Pedido? Pedido { get; set; }
}
