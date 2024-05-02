using Microsoft.AspNetCore.Mvc;

namespace SwitchSelect.Controllers
{
    public class DevolucaoController : Controller
    {
        public IActionResult IndexPedido(int id)
        {
           
            return View(id);
        }
    }
}
