using SwitchSelect.Data;
using SwitchSelect.Models.Grafico;

namespace SwitchSelect.Service;

public class GraficoVendasService
{
    private readonly SwitchSelectContext context;

    public GraficoVendasService(SwitchSelectContext context)
    {
        this.context = context;
    }

    public List<JogoGrafico> GetVendas(DateTime dataInicial, DateTime dataFinal)
    {
        var vendasPorJogo = (from pd in context.PedidoDetalhes
                             where pd.DataCompra >= dataInicial && pd.DataCompra <= dataFinal
                             
                             group pd by new { pd.JogoId, pd.NomeJogo, MesAno = new { pd.DataCompra.Year, pd.DataCompra.Month } }
                             into g
                             select new
                             {
                                 MesAno = new DateTime(g.Key.MesAno.Year, g.Key.MesAno.Month, 1),
                                 JogoNome = g.Key.NomeJogo,
                                 JogosValorTotal = g.Sum(v => v.Preco * v.Quantidade)
                             }).ToList();

        var lista = new List<JogoGrafico>();

        foreach (var item in vendasPorJogo)
        {
            var jogo = new JogoGrafico();
            jogo.JogoNome = item.JogoNome;
            jogo.JogosValor = item.JogosValorTotal;
            jogo.DataVenda = item.MesAno;
            lista.Add(jogo);
        }

        return lista;
    }

}
