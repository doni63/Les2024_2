using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Service;


namespace SwitchSelect.Controllers
{
    public class ClienteController : Controller
    {
        private readonly SwitchSelectContext _context;
        private readonly ClienteService _clienteService;

        public ClienteController(SwitchSelectContext context, ClienteService service)
        {
            _context = context;
            _clienteService = service;
        }

        public IActionResult ListaCliente()
        {
            var list = _clienteService.ListarClientes();
            return View(list);
        }

        public IActionResult PerfilUsuario()
        {
            return View();
        }

        public IActionResult PesquisarClientePorCpf()
        {
            return View();
        }

        public IActionResult AreaCliente(int id)
        {
            var cliente = _clienteService.ObterClientePorId(id);
            //var usuario = HttpContext.Session.GetString("usuario");
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult DadosPessoais(int id)
        {
            var cliente = _clienteService.ObterClientePorId(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClienteCompletoViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.CriarClienteAsync(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Edit(int? id, string origem)
        {
            ViewBag.Origem = origem;
            if (id == null)
            {
                return NotFound();
            }

            var clienteViewModel = _clienteService.ObterClientePorId(id.Value);
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ClienteCompletoViewModel clienteViewModel)
        {

            if (id != clienteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var sucesso = await _clienteService.EditarClienteAsync(id, clienteViewModel);
                if (sucesso)
                {
                    return RedirectToAction("AdminListaCliente", "Admin");
                }
                else
                {
                    return NotFound();
                }
            }
            return View(clienteViewModel);
        }

        public IActionResult EditDadosPessoais(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clienteViewModel = _clienteService.ObterClientePorIdDadosPessoais(id.Value);
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDadosPessoais(int id, [FromForm] ClienteDadosPessoaisViewModel clienteDadosPessoais)
        {
            if (id != clienteDadosPessoais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var sucesso = await _clienteService.EditarClienteDadosPessoais(id, clienteDadosPessoais);
                if (sucesso)
                {
                    return RedirectToAction("DadosPessoais", "Cliente", new { id = id });
                }
            }
            return View(clienteDadosPessoais);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            var viewModel = new ClienteDeleteViewModel
            {
                Id = cliente.Id,
                Nome = cliente.Nome
            };

            return View(viewModel);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clienteService.DeleteClienteAsync(id);
            return RedirectToAction("Index", "Home");
        }



    }
}
