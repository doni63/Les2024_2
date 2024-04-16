using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Models.Carrinho;
using SwitchSelect.Models.ViewModels;
namespace SwitchSelect.Components;

public class CarrinhoCompraResumo : ViewComponent
{
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
    {
        _carrinhoCompra = carrinhoCompra;
    }

    public IViewComponentResult Invoke()
    {
        var itens = _carrinhoCompra.GetCarrinhosCompraItens();
        //var itens = new List<CarrinhoCompraItem>()
        //{
        //    new CarrinhoCompraItem(),
        //    new CarrinhoCompraItem()
        //};
        _carrinhoCompra.CarrinhosCompraItens = itens;

        var carrinhoComprasVM = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
        };
        return View(carrinhoComprasVM);
    }
}
