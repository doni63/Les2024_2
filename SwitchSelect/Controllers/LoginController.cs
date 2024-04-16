using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;

namespace SwitchSelect.Controllers
{
    public class LoginController : Controller
    {
        private readonly SwitchSelectContext _context;

        public LoginController(SwitchSelectContext context)
        {
            _context = context;
        }

        public IActionResult TelaLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                //verificando cpf de usuário
                var usuario = _context.Clientes.FirstOrDefault(u => u.Cpf == model.Cpf);
                if (usuario != null) 
                {
                    HttpContext.Session.SetString("Cpf", usuario.Cpf);
                    HttpContext.Session.SetString("Nome", usuario.Nome);
                    HttpContext.Session.SetInt32("Id", usuario.Id);
                    return RedirectToAction("AreaCliente","Cliente", new { id = usuario.Id });
                }
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "CPF não encontrado");
                //}
            }
            return View("TelaLogin",model);
        }
    }
}
