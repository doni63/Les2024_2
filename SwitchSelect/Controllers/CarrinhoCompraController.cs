using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly SwitchSelectContext _context;
        
        public CarrinhoCompraController(IJogoRepositorio jogoRepositorio, CarrinhoCompra carrinhoCompra, SwitchSelectContext context)
        {
            _jogoRepositorio = jogoRepositorio;
            _carrinhoCompra = carrinhoCompra;
            _context = context;
           
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhosCompraItens();
            _carrinhoCompra.CarrinhosCompraItens = itens;

            var carrinhoComprasVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
                
            };
            return View(carrinhoComprasVM);
        }

        public IActionResult AdicionarItemNoCarrinhoCompra(int id)
        {
            var jogoSelecionado = _jogoRepositorio.Jogos.FirstOrDefault(j => j.Id == id);
            if(jogoSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(jogoSelecionado);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoverItemDoCarrinhoCompra(int id)
        {
            var jogoSelecionado = _jogoRepositorio.Jogos.FirstOrDefault(j => j.Id == id);
            if(jogoSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(jogoSelecionado);
            }
            return RedirectToAction("Index");
        }

       
        public IActionResult AplicarCupom(string codigoCupom)
        {
            var cupom = _context.Cupons.FirstOrDefault(c => c.CodigoCupom == codigoCupom && c.Status == "Válido");

            if (cupom != null)
            {
                // Obter os itens do carrinho do banco de dados
                var carrinhoCompraItens = _context.CarrinhoCompraItens
                    .Include(item => item.Jogo)
                    .Where(item => item.CarrinhoCompraId == _carrinhoCompra.CarrinhoCompraId)
                    .ToList();

                // Aplicar o desconto correspondente
                _carrinhoCompra.AplicarDesconto(cupom.Valor, carrinhoCompraItens);

                // Definir o cupom como usado
                cupom.Status = "Usado";
                _context.SaveChanges();

                // Redirecionar de volta à página do carrinho
                return RedirectToAction("Index");
            }
            else
            {
                // Se o cupom não for válido, retornar à página do carrinho com uma mensagem de erro
                TempData["ErroCupom"] = "O cupom inserido é inválido.";
                return RedirectToAction("Index");
            }
        }

    }
}
