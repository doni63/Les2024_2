using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Service;
using SwitchSelect.Models;
using SwitchSelect.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace SwitchSelect.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminservice;
        private readonly SwitchSelectContext _context;

        public AdminController(AdminService Adminservice, SwitchSelectContext context)
        {
            _adminservice = Adminservice;
            _context = context;
        }

        public IActionResult InativarCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                cliente.Status = "Inativo";
                _context.Update(cliente);
                _context.SaveChanges();
                return RedirectToAction("AdminListaCliente", "Admin", cliente);
            }
            else
            {
                ViewBag.Titulo = "Cliente não encontrado";
                ViewBag.Mensagem = "O cadastro do cliente não foi encontrado.";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
            
        }
        public IActionResult AtivarCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                cliente.Status = "Ativo";
                _context.Update(cliente);
                _context.SaveChanges();
                return RedirectToAction("AdminListaCliente","Admin", cliente);
            }
            else
            {
                ViewBag.Titulo = "Cliente não encontrado";
                ViewBag.Mensagem = "O cadastro do cliente não foi encontrado.";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
            
        }

        public IActionResult TelaAdmin()
        {
            return View();
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

        public IActionResult AdicionarProduto()
        {
            var categorias = _context.Categorias.ToList();
            SelectList listaCategorias = new SelectList(categorias, "Id", "Nome");
            ViewBag.Categorias = listaCategorias;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProduto([FromForm] Jogo jogo)
        {
            jogo.Categoria = _context.Categorias.FirstOrDefault(c => c.Id == jogo.CategoriaID);
            
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            return RedirectToAction("TelaAdmin", "Admin");
        }

        public IActionResult AdicionarCategoria()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarCategoria([FromForm] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction("TelaAdmin", "Admin");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListaVendas()
        {
            var list = await _adminservice.AdminListarPedidos();
            return View("ListaVendas", list);
        }

        [HttpPost]
        public IActionResult AtualizarStatus(int pedidoId, string novoStatus)
        {

            var pedido = _context.Pedidos.Find(pedidoId);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Status = novoStatus;
            _context.SaveChanges();
            return Ok();
        }

      
    }
}