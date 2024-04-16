using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwitchSelect.Models;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome da categoria")]
    [StringLength(50, ErrorMessage = "Número máximo de caracter 50")]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Preencha a descrição")]
    [StringLength(200, MinimumLength = 4, ErrorMessage = "A descrição deve ter entre 4 e 200 caracteres")]
    public string Descricao { get; set; }
    
    public List<Jogo> Jogos { get; set; } = new List<Jogo>();
}