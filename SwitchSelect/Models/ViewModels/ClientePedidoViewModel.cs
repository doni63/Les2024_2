using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models.ViewModels;
 
public class ClientePedidoViewModel
{
    //Informação de cliente
    public int Id { get; set; }
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
    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
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

    //informação pedidos
    public Pedido Pedido { get; set; }


}
