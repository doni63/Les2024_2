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

    public List<JogoGrafico> GetVendasCategoria(DateTime dataInicial, DateTime dataFinal)
    {
        var vendasPorJogo = (from pd in context.PedidoDetalhes
                             join j in context.Jogos on pd.JogoId equals j.Id
                             where pd.DataCompra >= dataInicial && pd.DataCompra <= dataFinal
                             group new { pd, j } by new { j.Categoria.Nome, MesAno = new { pd.DataCompra.Year, pd.DataCompra.Month } }
                             into g
                             select new
                             {
                                 MesAno = new DateTime(g.Key.MesAno.Year, g.Key.MesAno.Month, 1),
                                 Categoria = g.Key.Nome,
                                 JogosValorTotal = g.Sum(v => v.pd.Preco * v.pd.Quantidade)
                             }).ToList();

        var lista = new List<JogoGrafico>();

        foreach (var item in vendasPorJogo)
        {
            var jogo = new JogoGrafico();
            jogo.JogoNome = item.Categoria;
            jogo.JogosValor = item.JogosValorTotal;
            jogo.DataVenda = item.MesAno;
            lista.Add(jogo);
        }

        return lista;
    }

    public List<JogoGrafico> GetVendas(DateTime dataInicial, DateTime dataFinal)
    {
        var vendasPorJogo = (from pd in context.PedidoDetalhes
                             join j in context.Jogos on pd.JogoId equals j.Id
                             where pd.DataCompra >= dataInicial && pd.DataCompra <= dataFinal
                             group new { pd, j } by new { j.Nome, MesAno = new { pd.DataCompra.Year, pd.DataCompra.Month } }
                             into g
                             select new
                             {
                                 MesAno = new DateTime(g.Key.MesAno.Year, g.Key.MesAno.Month, 1),
                                 Nome = g.Key.Nome,
                                 JogosValorTotal = g.Sum(v => v.pd.Preco * v.pd.Quantidade)
                             }).ToList();

        var lista = new List<JogoGrafico>();

        foreach (var item in vendasPorJogo)
        {
            var jogo = new JogoGrafico();
            jogo.JogoNome = item.Nome;
            jogo.JogosValor = item.JogosValorTotal;
            jogo.DataVenda = item.MesAno;
            lista.Add(jogo);
        }

        return lista;
    }

}
