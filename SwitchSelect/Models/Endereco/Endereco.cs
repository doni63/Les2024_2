using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TipoLogradouro TipoLogradouro { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Limite 100 caracteres")]
        public string Logradouro { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Limite 10 caracteres")]
        public string Numero { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Limite 8 caracteres")]
        public string CEP { get; set; }
        public virtual Bairro Bairro { get; set; }
        public int BairroID {  get; set; }

        [StringLength(200, ErrorMessage = "Limite 200 caracteres")]
        public string? Complemento { get; set; }
       
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public TipoEndereco TipoEndereco { get; set;}
        public TipoResidencia TipoResidencia { get; set; }


    }

}
