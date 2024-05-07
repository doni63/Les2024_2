using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Controllers
{
    public class TrocaController : Controller
    {
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly SwitchSelectContext _context;

        public TrocaController(IJogoRepositorio jogoRepositorio, SwitchSelectContext context)
        {
            _jogoRepositorio = jogoRepositorio;
            _context = context;
        }

        public IActionResult TrocaPedido(int pedidoId)
        {
            var produtosIds = _context.PedidoDetalhes
                              .Where(pi => pi.PedidoId == pedidoId)
                              .Select(pi => pi.JogoId)
                              .ToList();

            var viewModel = new TrocaProdutoViewModel
            {
                PedidoId = pedidoId,
                ProdutosIds = produtosIds
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult TrocaPedido([FromForm] TrocaProdutoViewModel viewModel, string motivo)
        {
            int pedidoId = viewModel.PedidoId;
            List<int> produtosIds = viewModel.ProdutosIds;

            var produtos = _context.PedidoDetalhes
                           .Where(pi => produtosIds.Contains(pi.JogoId))
                           .ToList();

            foreach (var produto in produtos)
            {
                var troca = new TrocaProduto();
                troca.Motivo = viewModel.Motivo;
                troca.JogoId = produto.JogoId;
                troca.NomeJogo = produto.NomeJogo;
                troca.PedidoId = pedidoId;
                troca.Status = "Troca solicitada";

                _context.TrocaProdutos.Add(troca);
            }
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == pedidoId);
            if (pedido != null)
            {
                pedido.Status = "EM TROCA";
                _context.Update(pedido);
            }
            _context.SaveChanges();
            return View("TrocaConfirmacao");
        }

        [HttpPost]
        public IActionResult SolicitarTrocaProduto([FromForm] PedidoDetalhe model, string motivo, int qtd)
        {
            //buscar dados do jogo para troca
            var jogoTroca = _jogoRepositorio.GetJogoPorId(model.JogoId);
            //atualizar restricao do pedido do produto
            var detalhe = _context.PedidoDetalhes.FirstOrDefault(d => d.JogoId == model.JogoId);

            if (detalhe != null)
            {
                detalhe.Restricao = "EM TROCA";
                _context.Update(detalhe);
            }
           
            var trocaJogo = new TrocaProduto();
            trocaJogo.Motivo = motivo;
            trocaJogo.JogoId = model.JogoId;
            trocaJogo.NomeJogo = jogoTroca.Nome;
            trocaJogo.PedidoId = model.PedidoId;
            trocaJogo.Status = "Troca solicitada";
            trocaJogo.DataSolicitacao = DateTime.Now;
            trocaJogo.Qtd = qtd;
            trocaJogo.Valor = jogoTroca.Preco * qtd;
            
            
            _context.TrocaProdutos.Add(trocaJogo);
            _context.SaveChanges();



            return View("~/Views/Pedido/TrocaSolicitada.cshtml");
        }
    }
}
