using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Models;
using SwitchSelect.Models.Carrinho;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Controllers
{
    
    public class PedidoController : Controller
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepositorio pedidoRepositorio, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult FinalizarPedido()
        {
            return View();
        }
        public IActionResult Pedido()
        {
            return View();
        }
        public IActionResult StatusPedido()
        {
            return View();
        }
        public IActionResult TrocaPedido()
        {
            return View();
        }

        public IActionResult StatusTroca()
        {
            return View();
        }

        public IActionResult DevolucaoPedido()
        {
            return View();
        }

        public IActionResult StatusDevolucao()
        {
            return View();
        }

        public IActionResult PedidoList()
        {
            return View();
        }

        [HttpGet]
       public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(PedidoController pedido)
        {
            return View();
        }

    }
}
