using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;
using SwitchSelect.Service;

namespace SwitchSelect.Controllers
{
    public class CartaoController : Controller
    {
        private readonly ICartaoRepositorio _cartaoRepositorio;
        private readonly CartaoService _cartaoService;

        public CartaoController(CartaoService service, ICartaoRepositorio cartaoRepositorio)
        {
            _cartaoService = service;
            _cartaoRepositorio = cartaoRepositorio;
        }

        public IActionResult Create(int clienteId)
        {
            var viewModel = new CartaoViewModel
            {
                ClienteId = clienteId
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CartaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _cartaoService.CriarCartaoAsync(model);
            return RedirectToAction(nameof(CartaoList), new { clienteId = model.ClienteId });
        }
        public IActionResult CartaoList(int clienteId)
        {
            var cartoes = _cartaoRepositorio.ObterCartaoPorCliente(clienteId);

            //convertendo model para viewmodel
            var cartoesViewModel = cartoes.Select(c => new CartaoViewModel
            {
                Id = c.Id,
                ClienteId = c.ClienteId,
                NumeroCartao = c.NumeroCartao,
                TitularDoCartao = c.TitularDoCartao,
                CpfTitularCartao = c.CpfTitularCartao,
                MesValidade = c.MesValidade,
                AnoValidade = c.AnoValidade,
                DataValidade = c.DataValidade,
                CVV = c.CVV,
                TipoCartao = c.TipoCartao,
            }).ToList();
            if(cartoesViewModel is null)
            {
                return NotFound();
            }
            foreach(var cartao in cartoesViewModel)
            {
                var numeroCartao = cartao.NumeroCartao;
                cartao.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);
            }
            ViewData["ClienteID"] = clienteId;
            return View(cartoesViewModel);
        }

        public IActionResult Edit(int? id, int clienteId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cartaoViewModel = _cartaoService.ObterCartaoPorId(id.Value);
            cartaoViewModel.ClienteId = clienteId;
            if(cartaoViewModel is null)
            {
                return NotFound();
            }
            return View(cartaoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] CartaoViewModel cartaoViewModel)
        {
            try
            {
                if (id != cartaoViewModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var sucesso = await _cartaoService.EditCartao(id, cartaoViewModel);
                    if (sucesso)
                    {
                        var clienteId = cartaoViewModel.ClienteId;
                        return RedirectToAction("CartaoList", new { clienteId = clienteId });
                    }
                    else
                    {
                        // Retornar uma view de erro personalizada com uma mensagem específica
                        
                        return View("Error");
                    }
                }
                return View(cartaoViewModel);
            }
            catch (Exception ex)
            {
                // Logue a exceção para registrar detalhes
                Console.WriteLine(ex);
                ViewData["ErrorMessage"] = ex;
                // Passe a mensagem de exceção como modelo para a view de erro
                return RedirectToAction("Error", new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id, int clienteId)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }


            var cartao = _cartaoRepositorio.Cartoes.FirstOrDefault(c => c.Id == id);

            if(cartao == null)
            {
                return NotFound();
            }
            var numeroCartao = cartao.NumeroCartao;
            var cartaoViewModel = new CartaoViewModel
            {
                Id = cartao.Id,
                ClienteId = clienteId,
                CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao),
            };
            return View(cartaoViewModel);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmacao(int id, int clienteId)
        {
            await _cartaoService.DeleteCartao(id);
            return RedirectToAction(nameof(CartaoList), new { clienteId = clienteId });
        }

    }
}
