using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwitchSelect.Models;

[Table("Clientes")]
public class Cliente
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Informe o nome")]
    [StringLength(100, ErrorMessage = "Número máximo de caracter 100")]
    public string Nome { get; set; }

    [Display(Name = "Data de Nascimento")]
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
    [Display(Name ="CPF")]
    public string Cpf { get; set; }
    [Required(ErrorMessage = "Informe o RG")]
    [StringLength(9, ErrorMessage = "Número máximo de caracter 9")]
    public string RG { get; set; }
    public List <Telefone> Telefones { get; set; }
    public List<Endereco> Enderecos { get; set; }
    public List<Cartao> Cartoes { get; set; } 

    public List<Cupom>? Cupons { get; set; } 

}
