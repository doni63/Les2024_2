using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Models;

namespace SwitchSelect.Controllers
{
    public class PedidoController : Controller
    {
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

       
    }
}
