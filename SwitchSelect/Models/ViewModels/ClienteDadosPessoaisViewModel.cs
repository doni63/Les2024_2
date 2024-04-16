using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models.ViewModels
{
    public class ClienteDadosPessoaisViewModel
    {
        // Informações do Cliente
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(100, ErrorMessage = "Número máximo de caracter 100")]
        public string Nome { get; set; }

        [Required]
        public DateTime DataDeNascimento { get; set; }

        [Required]
        [StringLength(10)]
        public string Genero { get; set; }


        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o Cpf")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Cpf inválido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o RG")]
        [StringLength(9, ErrorMessage = "Número máximo de caracter 9")]
        public string RG { get; set; }

        //informação de telefone
        public TipoTelefone TipoTelefone { get; set; }
        [Required(ErrorMessage = "Informe DDD")]
        [StringLength(3)]
        public string DDD { get; set; }

        [Required(ErrorMessage = "Informe Telefone")]
        [StringLength(9)]
        [Display(Name = "Telefone")]
        public string NumeroTelefone { get; set; }
    }
}
