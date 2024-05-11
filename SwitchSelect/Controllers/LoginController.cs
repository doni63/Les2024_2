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
            
            if (ModelState.IsValid)
            {
                //verificando cpf de usuário
                if (model.Cpf != null)
                {
                    var cliente = _cliRepo.GetPorCpf(model.Cpf);

                    if (cliente.Status.Equals("Ativo"))
                    {
                       
                        return RedirectToAction("AreaCliente", "Cliente", cliente);
                    }
                    else
                    {
                        ViewBag.Titulo = "Cliente Bloqueado.";
                        ViewBag.Mensagem = "Entre em contato com nossa equipe.";
                        return View("~/Views/Mensagem/Mensagem.cshtml");
                    }
                    
                }
                else
                {
                    ViewBag.Titulo = "Cadastro não encontrado";
                    ViewBag.Mensagem = "Cpf não foi encontrado em nossa base de dados. É necessário se cadastrar primeiro.";
                    return View("~/Views/Mensagem/Mensagem.cshtml");
                }
            }
            return View("TelaLogin", model);
        }
    }
}
