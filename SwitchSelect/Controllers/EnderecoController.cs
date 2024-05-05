using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;
using SwitchSelect.Service;

namespace SwitchSelect.Controllers;

public class EnderecoController : Controller
{
    private readonly IEnderecoRepositorio _enderecoRepositorio;
    private readonly EnderecoService _enderecoService;
    private readonly SwitchSelectContext _context;
    private readonly IClienteRepositorio _clienteRepositorio;

    public EnderecoController(IEnderecoRepositorio enderecoRepositorio, EnderecoService enderecoService, SwitchSelectContext context, IClienteRepositorio clienteRepositorio)
    {
        _enderecoRepositorio = enderecoRepositorio;
        _enderecoService = enderecoService;
        _context = context;
        _clienteRepositorio = clienteRepositorio;
    }


    public IActionResult EnderecoList(int clienteId)
    {
        var enderecos = _enderecoRepositorio.ObterEnderecosPorCliente(clienteId);

        // Convertendo de Model para ViewModel
        var enderecosViewModel = enderecos.Select(e => new EnderecoViewModel
        {
            Id = e.Id,
            ClienteID = clienteId,
            Logradouro = e.Logradouro,
            Numero = e.Numero,
            Complemento = e.Complemento,
            Bairro = e.Bairro.Descricao,
            Cidade = e.Bairro.Cidade.Descricao,
            Estado = e.Bairro.Cidade.Estado.Descricao,
            CEP = e.CEP

        }).ToList();
        ViewData["ClienteID"] = clienteId;
        return View(enderecosViewModel);
    }

    public IActionResult Create(int clienteId, string origem)
    {
        var viewModel = new EnderecoViewModel
        {
            ClienteID = clienteId
        };
        viewModel.Origem = origem;
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EnderecoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _enderecoService.CriarEnderecoAsync(model);

        if (model.Origem != null)
        {
            if (model.Origem.Equals("Pedido"))
            {
                var cliente = _clienteRepositorio.GetPorId(model.ClienteID);

                var precoTotalPedidoBytes = HttpContext.Session.Get("PrecoTotalPedido");
                var totalItensPedidoBytes = HttpContext.Session.Get("TotalItensPedido");

                // Converter os bytes de volta para os tipos de dados originais
                var precoTotalPedido = BitConverter.ToDouble(precoTotalPedidoBytes);
                var totalItensPedido = BitConverter.ToInt32(totalItensPedidoBytes);

                ViewBag.PrecoTotalPedido = precoTotalPedido;
                ViewBag.TotalItensPedido = totalItensPedido;

                return View("~/Views/Pedido/Checkout.cshtml", cliente);
            }
            // Redireciona para a lista de endereços do cliente, passando o clienteId
            return RedirectToAction(nameof(EnderecoList), new { clienteId = model.ClienteID });
        }

        // Redireciona para a lista de endereços do cliente, passando o clienteId
        return RedirectToAction(nameof(EnderecoList), new { clienteId = model.ClienteID });
    }

    [HttpGet]
    public IActionResult Delete(int? id, int clienteId)
    {
        if (id == null)
        {
            return NotFound();
        }
        var endereco = _enderecoRepositorio.Enderecos
            .FirstOrDefault(e => e.Id == id);
        if (endereco == null) { return NotFound(); }

        var viewModel = new EnderecoViewModel
        {
            Id = endereco.Id,
            ClienteID = clienteId,
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero

        };
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmedAsync(int id, int clienteId)
    {
        await _enderecoService.DeleteEndereco(id);
        // Redireciona para a lista de endereços do cliente, passando o clienteId
        return RedirectToAction(nameof(EnderecoList), new { clienteId = clienteId });
    }

    public IActionResult Edit(int? id, int clienteId)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enderecoViewModel = _enderecoService.ObterEnderecoPorId(id.Value);
        enderecoViewModel.ClienteID = clienteId;
        if (enderecoViewModel is null)
        {
            return NotFound();
        }

        return View(enderecoViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(int id, [FromForm] EnderecoViewModel enderecoViewModel)
    {

        if (id != enderecoViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var sucesso = await _enderecoService.EditEnderecoAsync(id, enderecoViewModel);

            if (sucesso)
            {

                var clienteId = enderecoViewModel.ClienteID;
                return RedirectToAction("EnderecoList", "Endereco", new { clienteId = clienteId });
                // return View("EnderecoList", enderecos);
            }
            else
            {
                return NotFound();
            }
        }

        return View(enderecoViewModel);
    }

}
