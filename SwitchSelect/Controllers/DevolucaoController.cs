using Microsoft.AspNetCore.Mvc;

namespace SwitchSelect.Controllers
{
    public class DevolucaoController : Controller
    {
        public IActionResult Index(int id)
        {
           
            return View(id);
        }
    }
}
