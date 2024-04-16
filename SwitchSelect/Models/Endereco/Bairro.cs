using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models
{
    [Table("Bairro")]
    public class Bairro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name ="Bairro")]
        public string Descricao { get; set; }
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();

    }
}
