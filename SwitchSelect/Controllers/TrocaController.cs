using Microsoft.AspNetCore.Mvc;
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

        public IActionResult SolicitarTroca(int jogoId, string nomeJogo, int pedidoId)
        {
            var trocaViewModel = new TrocaProdutoViewModel
            {
                JogoId = jogoId,
                NomeJogo = nomeJogo,
                PedidoId = pedidoId
                
            };
           
            
            return View(trocaViewModel);
        }

        [HttpPost]
        public IActionResult SolicitarTroca([FromForm] TrocaProdutoViewModel viewModel)
        {
            //atualizando status do pedido
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == viewModel.PedidoId);
            if (pedido != null)
            {
                pedido.Status = "EM TROCA";
                _context.Update(pedido);
                _context.SaveChanges();
            }

            var troca = new TrocaProduto
            {
                Motivo = viewModel.Motivo,
                JogoId= viewModel.JogoId,
                NomeJogo = viewModel.NomeJogo,
                PedidoId = viewModel.PedidoId,
                Status = "EM TROCA"
            };
           
            _context.TrocaProdutos.Add(troca);
            _context.SaveChanges();
            return View("TrocaConfirmacao");
        }

        //public async Task<IActionResult> TrocaListCliente(int clienteId)
        //{

        //} 
    }
}
