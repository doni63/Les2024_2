using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models
{
    [Table("Estados")]
    public class Estado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe Estado")]
        [Display(Name ="Estado")]
        public string Descricao { get; set; }

        public List<Cidade> Cidades { get; set; } = new List<Cidade>();
    }
}
