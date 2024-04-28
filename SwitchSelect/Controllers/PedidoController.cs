using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;
using SwitchSelect.Service;

namespace SwitchSelect.Controllers
{

    public class PedidoController : Controller
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly SwitchSelectContext _context;
        private readonly CartaoService _cartaoService;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ClienteService _clienteService;


        public PedidoController(IPedidoRepositorio pedidoRepositorio, CarrinhoCompra carrinhoCompra, SwitchSelectContext context, CartaoService cartaoService, ClienteService clienteService, IClienteRepositorio clienteRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _carrinhoCompra = carrinhoCompra;
            _context = context;
            _cartaoService = cartaoService;
            _clienteRepositorio = clienteRepositorio;
            _clienteService = clienteService;

        }
        [HttpGet]
        public IActionResult CheckoutLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckoutCpf(LoginViewModel model)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0;

            if (ModelState.IsValid)
            {
                //obter itens do carrinho de compra do cliente
                List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhosCompraItens();
                _carrinhoCompra.CarrinhosCompraItens = itens;
                if (_carrinhoCompra.CarrinhosCompraItens.Count == 0)
                {
                    ModelState.AddModelError("", "Carrinho vazio");
                }

                //calcular total de pedidos e de itens
                foreach (var item in itens)
                {
                    totalItensPedido += item.Quantidade;
                    precoTotalPedido += (item.Jogo.Preco * item.Quantidade);
                }

                ViewBag.PrecoTotalPedido = precoTotalPedido;
                ViewBag.TotalItensPedido = totalItensPedido;

                if (model.Cpf != null)
                {
                    var cliente = _clienteRepositorio.GetPorCpf(model.Cpf);
                    foreach (var cartao in cliente.Cartoes)
                    {
                        var numeroCartao = cartao.NumeroCartao;
                        cartao.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);
                    }
                    //colocar cliente na sessao
                    _clienteService.GuardarClienteNaSessao(cliente);
                    return View("Checkout", cliente);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "CPF não encontrado");
                }
            }
            return View();
        }



        [HttpPost]
        public IActionResult Checkout(int enderecoId, int cartaoId, decimal precoTotalPedido, int totalItensPedido, string cupomAplicado)
        {
            //buscando cliente da sessão
            var cliente = _clienteService.ObterClienteDaSessao();
            

            //se cupom for aplicado obter cupom para editar clienteId e status
            if(cliente != null)
            {
                var cupom = _context.Cupons.FirstOrDefault(c => c.CodigoCupom == cupomAplicado);
                cupom.Status = "Usado";
                cupom.ClienteId = cliente.Id;
                _context.Update(cupom);
                _context.SaveChanges();
            }
            


            //obter itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhosCompraItens();

            //valor do desconto
            decimal totalValoPedido = 0m;
            foreach (var item in itens)
            {
                totalItensPedido += item.Quantidade;
                totalValoPedido += (item.Jogo.Preco * item.Quantidade);
            }
            var desconto = totalValoPedido - precoTotalPedido;

            var modelPedido = new Pedido();
                      
            //dados Pedido
            modelPedido.ClienteId = cliente.Id;

            //modelPedido.Cliente = cliente;
            modelPedido.Nome = cliente.Nome;
            modelPedido.TelefoneId = cliente.Telefones.FirstOrDefault().Id;
            modelPedido.EnderecoId = enderecoId;
            modelPedido.cartaoId = cartaoId;

            modelPedido.Bandeira = cliente.Cartoes.FirstOrDefault(c => c.Id == cartaoId)
                .Bandeira.ToString();

            string numeroCartao = cliente.Cartoes.FirstOrDefault(c => c.Id == cartaoId).NumeroCartao.ToString();

            modelPedido.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);

            modelPedido.Status = "Processando";
            modelPedido.TotalItensPedido = totalItensPedido;
            modelPedido.PedidoTotal = precoTotalPedido;
            modelPedido.Desconto = desconto;

            //valida os dados do pedido
            if (ModelState.IsValid)
            {
                //cria os pedidos e os detalhes

                _pedidoRepositorio.CriarPedido(modelPedido);

                //mensagem ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pela compra !";
                ViewBag.PedidoTotal = precoTotalPedido;

                //limpa o carrinho 
                _carrinhoCompra.LimparCarrinho();
                _clienteService.LimparSessaoCliente();

                //exibir a view com dados de cliente e pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", modelPedido);
            }
            return View(cliente);

        }

        public IActionResult AplicarCupom(string Cpf, string codigoCupom)
        {
            ViewBag.Cupom = codigoCupom;
            //recebendo valor do desconto
            var cupom = _context.Cupons.FirstOrDefault(c => c.CodigoCupom == codigoCupom);
            decimal desconto = 0m;

            //adicionando o valor do desconto na variavel desconto
            if (cupom != null && cupom.Status.Equals("Valido"))
            {
                desconto = cupom.Valor;
            }

            //atualizar valor com desconto

            int totalItensPedido = 0;
            decimal precoTotalPedido = 0;

            //obter itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhosCompraItens();
            _carrinhoCompra.CarrinhosCompraItens = itens;
            if (_carrinhoCompra.CarrinhosCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Carrinho vazio");
            }
            //calcular total de pedidos e de itens
            foreach (var item in itens)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Jogo.Preco * item.Quantidade);
            }
            //aplicando desconto
            precoTotalPedido = precoTotalPedido - desconto;

            ViewBag.PrecoTotalPedido = precoTotalPedido;
            ViewBag.TotalItensPedido = totalItensPedido;

            if (Cpf != null)
            {
                var cliente = _clienteRepositorio.GetPorCpf(Cpf);
                foreach (var cartao in cliente.Cartoes)
                {
                    var numeroCartao = cartao.NumeroCartao;
                    cartao.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);
                }
                //colocar cliente na sessao
                _clienteService.GuardarClienteNaSessao(cliente);
                return View("Checkout", cliente);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "CPF não encontrado");
            }

            return View();
        }

    }
}
