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
        pedido.PedidoEnviado = DateTime.Now;
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoCompraItens = _carrinhoCompra.CarrinhosCompraItens;

        //criando código único do produto vendido


        foreach (var carrinhoItens in carrinhoCompraItens)
        {

            var pedidoDetalhe = new PedidoDetalhe()
            {
                Quantidade = carrinhoItens.Quantidade,
                JogoId = carrinhoItens.Jogo.Id,
                Preco = carrinhoItens.Jogo.Preco,
                NomeJogo = carrinhoItens.Jogo.Nome,
                ImagemUrl = carrinhoItens.Jogo.ImagemUrl,
                PedidoId = pedido.Id,
                Restricao = "Nenhum",
            };
            _context.PedidoDetalhes.Add(pedidoDetalhe);
        }
    }
}
