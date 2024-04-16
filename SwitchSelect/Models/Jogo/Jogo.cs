using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models;


[Table("Jogos")]
public class Jogo
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do jogo")]
    [StringLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe o Preço do jogo")]
    [Display(Name = "Preço")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 999.99, ErrorMessage = "O valor deve estar ente 1 e 999,99")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "Informe o caminho da imagem")]
    [StringLength(100, ErrorMessage = "O tamanho máximo é de 100 cacteres")]
    [Display(Name = "Caminho da imagem")]
    public string ImagemUrl { get; set; }

    [Display(Name = "Preferido")]
    public bool? IsJogoPreferido { get; set; }

    [Display(Name = "Estoque")]
    public bool? EmEstoque { get; set; }
   
    public int CategoriaID { get; set; }
    public virtual Categoria Categoria { get; set; }


}
