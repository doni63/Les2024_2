using Microsoft.AspNetCore.Mvc;

namespace SwitchSelect.Controllers
{
    public class TesteController : Controller
    {
        public string Index()
        {
            return $"Testando rotas, método teste : {DateTime.Now}";
        }
    }
}
