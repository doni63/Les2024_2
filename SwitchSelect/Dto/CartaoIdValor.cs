using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchSelect.Dto;

[NotMapped]
public class CartaoIdValor
{
    public int Id { get; set; }
    public decimal Valor { get; set; }
}
