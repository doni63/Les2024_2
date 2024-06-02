using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Repositorios;

public class PedidoRepositorio : IPedidoRepositorio
{
    private readonly SwitchSelectContext _context;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoRepositorio(SwitchSelectContext context, CarrinhoCompra carrinhoCompra)
    {
        _context = context;
        _carrinhoCompra = carrinhoCompra;
    }

    public void CriarPedido(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoCompraItens = _carrinhoCompra.CarrinhosCompraItens;

        foreach (var carrinhoItens in carrinhoCompraItens)
        {

            var pedidoDetalhe = new PedidoDetalhe()
            {
                Quantidade = carrinhoItens.Quantidade,
                JogoId = carrinhoItens.Jogo.Id,
                Preco = carrinhoItens.Jogo.Preco,
                NomeJogo = carrinhoItens.Jogo.Nome,
                DataCompra = DateTime.Now,
                ImagemUrl = carrinhoItens.Jogo.ImagemUrl,
                PedidoId = pedido.Id,
                Restricao = "Nenhum",
            };
            _context.PedidoDetalhes.Add(pedidoDetalhe);
        }
    }
}
