using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models
{
    [Table("Cidades")]
    public class Cidade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name ="Cidade")]
        public string Descricao { get; set; }

        public virtual Estado Estado { get; set; }
        public int EstadoID { get; set; }
        public List<Bairro> Bairros { get; set; }

    }
}
