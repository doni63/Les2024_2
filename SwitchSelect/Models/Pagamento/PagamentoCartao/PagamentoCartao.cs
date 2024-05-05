namespace SwitchSelect.Models;

public class PagamentoCartao
{
    public int Id { get; set; }
    public int? PagamentoId { get; set; }
    public Pagamento? Pagamento { get; set; }

    public int CartaoId { get; set; }
    public Cartao? Cartao { get; set; }
}
