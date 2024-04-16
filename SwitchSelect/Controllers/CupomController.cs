using Microsoft.AspNetCore.Mvc;

namespace SwitchSelect.Controllers
{
    public class CupomController : Controller
    {
        public IActionResult CupomList()
        {
            return View();
        }
    }
}
