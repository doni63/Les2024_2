using Microsoft.AspNetCore.Mvc;

namespace SwitchSelect.Controllers
{
    public class CarrinhoDeCompraController : Controller
    {
       public IActionResult Carrinho()
        {
            return View();
        }
       
    }
}
