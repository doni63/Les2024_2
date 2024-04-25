using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Controllers
{
    public class LoginController : Controller
    {
        private readonly SwitchSelectContext _context;
        private readonly IClienteRepositorio _cliRepo;

        public LoginController(SwitchSelectContext context, IClienteRepositorio cliRepo)
        {
            _context = context;
            _cliRepo = cliRepo;
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
                //var usuario = _context.Clientes.FirstOrDefault(u => u.Cpf == model.Cpf);
                if (model.Cpf != null) 
                {
                    //HttpContext.Session.SetString("Cpf", usuario.Cpf);
                    //HttpContext.Session.SetString("Nome", usuario.Nome);
                    //HttpContext.Session.SetInt32("Id", usuario.Id);
                    var cliente = _cliRepo.GetPorCpf(model.Cpf);
                    //return RedirectToAction("AreaCliente","Cliente", new { id = usuario.Id });
                    return RedirectToAction("AreaCliente","Cliente",cliente);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "CPF não encontrado");
                }
            }
            return View("TelaLogin",model);
        }
    }
}
