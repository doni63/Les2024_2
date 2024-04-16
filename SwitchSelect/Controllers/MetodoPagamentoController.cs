using Microsoft.AspNetCore.Mvc;

namespace SwitchSelect.Controllers
{
    public class MetodoPagamentoController : Controller
    {
        public IActionResult MetodoPagamento()
        {
            return View();
        }
    }
}
