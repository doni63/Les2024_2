using Microsoft.AspNetCore.Mvc;

namespace SwitchSelect.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
