namespace SwitchSelect.Dto;

public class TrocaProdutosRequest
{
    public Dictionary<int, int> Quantidade { get; set; }
    public Dictionary<int, string> Motivo { get; set; }
}
