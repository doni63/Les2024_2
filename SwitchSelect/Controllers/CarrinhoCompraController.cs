using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Models.Carrinho;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(IJogoRepositorio jogoRepositorio, CarrinhoCompra carrinhoCompra)
        {
            _jogoRepositorio = jogoRepositorio;
            _carrinhoCompra = carrinhoCompra;
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

        
    }
}
