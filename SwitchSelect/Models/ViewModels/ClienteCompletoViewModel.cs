
using SwitchSelect.Service;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwitchSelect.Models.ViewModels
{
    public class ClienteCompletoViewModel
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
        [Display(Name = "CPF")]
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
        [Display(Name ="Telefone")]
        public string NumeroTelefone { get; set; }

        // Informações de Endereço
        
        [Required]
        [Display(Name ="Tipo de endereço")]
        public TipoEndereco TipoEndereco { get; set; }

        [Required]
        [Display(Name ="Tipo de residência")]
        public TipoResidencia TipoResidencia { get; set; }

        [Required]
        public TipoLogradouro TipoLogradouro { get; set; }

        [Required(ErrorMessage = "Informe o logradouro")]
        [StringLength(100, ErrorMessage = "Limite de 100 caracteres para o logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Informe o número")]
        [StringLength(10, ErrorMessage = "Limite de 10 caracteres para o número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [StringLength(8, ErrorMessage = "Limite de 8 caracteres para o CEP")]
        public string CEP { get; set; }

        [StringLength(200, ErrorMessage = "Limite de 200 caracteres para o complemento")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        [StringLength(100)]
        public string Bairro { get; set; }

       [Required(ErrorMessage = "Informe a cidade")]
       [StringLength(100)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o Estado")]
        [StringLength(100)]
        public string Estado { get; set; }

        //Dados do cartão
        [Required(ErrorMessage = "Informe número do cartão")]
        [Display(Name = "Numero do cartão")]
        [StringLength(16)]
        public string NumeroCartao { get; set; }

        [NotMapped]
        [Display(Name = "Cartao de final")]
        public string? CartaoQuatroDigito { get; set; }

        [Required(ErrorMessage = "Informe nome do titular do cartão")]
        [Display(Name = "Nome do titular")]
        [StringLength(100)]
        public string TitularDoCartao { get; set; }

        [Required(ErrorMessage = "Informe cpf do titular")]
        [Display(Name = "Cpf do titular")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Cpf inválido")]
        public string CpfTitularCartao { get; set; }

        
        [Required(ErrorMessage = "Informe mês de validade do cartão")]
        [Range(1, 12, ErrorMessage = "O mês de validade deve estar entre 1 e 12.")]
        [MesValidade(ErrorMessage = "O mês de validade deve ser maior que o mês atual.")]
        [NotMapped]
        public int MesValidade { get; set; }

        [Required(ErrorMessage = "O ano de validade é obrigatório.")]
        [AnoValidade]
        [NotMapped]
        public int AnoValidade { get; set; }

        
        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade")]
        public DateTime? DataValidade { get; set; }

        [Required(ErrorMessage = "Informe o CVV")]
        [Display(Name = "CVV")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV inválido")]
        public string CVV { get; set; }

        [Required]
        [Display(Name = "Tipo de cartão")]
        public TipoCartao TipoCartao { get; set; }

    }
}
