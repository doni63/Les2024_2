using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Service;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;

namespace SwitchSelect.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminservice;

        public IActionResult TelaAdmin()
        {
            return View();
        }

        public AdminController(AdminService Adminservice)
        {
            _adminservice = Adminservice;

        }

        public IActionResult AdminListaCliente(string pesquisa)
        {
            if (String.IsNullOrEmpty(pesquisa))
            {
                var list = _adminservice.AdminListarClientes();
                return View(list);
            }
            else
            {
                var list = _adminservice.AdminPesquisarCliente(pesquisa);
                return View(list);
            }
        }

        public async Task<IActionResult> AdminInformacaoCliente(int? id)
        {
            if (id == null) { return NotFound(); }

            var cliente = await _adminservice.AdminGetCliente(id.Value);
            if (cliente == null) { return NotFound(); };
            return View(cliente);
        }

    }
}
