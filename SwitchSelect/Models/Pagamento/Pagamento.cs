using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }

        [StringLength(20)]
        public string Tipo { get; set; } // Pode ser cartão de crédito ou cupom

        public List<string> NumerosCartao { get; set; } // Lista de números de cartão associados ao pagamento

        public List<Cupom> Cupons { get; set; } // Lista de cupons associados ao pagamento

        [StringLength(14)]
        public string? CodigoCupom { get; set; }

        
    }
}
