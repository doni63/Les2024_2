using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models
{
    [Table("Telefones")]
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string DDD { get; set; }

        [Required(ErrorMessage = "Informe o seu telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string NumeroTelefone { get; set; }

        public TipoTelefone TipoTelefone { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
