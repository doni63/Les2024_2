using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models.ViewModels;

public class EnderecoViewModel
{
    [Display(Name ="Id")]
    public int Id { get; set; }
    public int ClienteID { get; set; }

    [Required(ErrorMessage ="Informe tipo de endereço")]
    
    [Display(Name = "Tipo de endereço")]
    public TipoEndereco TipoEndereco { get; set; }

    [Required(ErrorMessage ="Informe o tipo de residência")]
    [Display(Name = "Tipo de residência")]
    public TipoResidencia TipoResidencia { get; set; }

    [Required(ErrorMessage ="Informe o tipo de logradouro")]
    [Display(Name ="Tipo de logradoutro")]
    public TipoLogradouro TipoLogradouro { get; set; }


    [Required(ErrorMessage ="Informe o logradouro")]
    [StringLength(100, ErrorMessage = "Limite 100 caracteres")]
    public string Logradouro { get; set; }

    [Display(Name ="Número")]
    [Required(ErrorMessage ="Informe o número")]
    [StringLength(10, ErrorMessage = "Limite 10 caracteres")]
    public string Numero { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "Limite 8 caracteres")]
    public string CEP { get; set; }

    public string? Complemento { get; set; }

    [Required(ErrorMessage ="Informe o Bairro")]
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


}
