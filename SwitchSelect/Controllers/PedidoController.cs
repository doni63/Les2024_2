using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CheckoutCpf()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckoutCpf(LoginViewModel model)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

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

                //string cpf = model.Cpf;

                if (model.Cpf != null)
                {
                    var cliente = _clienteRepositorio.GetPorCpf(model.Cpf);
                    foreach(var cartao in cliente.Cartoes)
                    {
                        var numeroCartao = cartao.NumeroCartao;
                        cartao.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);
                    }
                    _clienteService.GuardarClienteNaSessao(cliente);
                    return View("Checkout", cliente);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "CPF não encontrado");
                }
            }
            return View(model);
        }
        [HttpGet]
       

        [HttpPost]
        public IActionResult Checkout(int enderecoId,int cartaoId,decimal precoTotalPedido, int totalItensPedido)
        {
            
            //obter itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhosCompraItens();

            var modelPedido = new Pedido();

            //buscando cliente da sessão
            var cliente = _clienteService.ObterClienteDaSessao();

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

            //valida os dados do pedido
            if (ModelState.IsValid)
            {
                //cria os pedidos e os detalhes

                _pedidoRepositorio.CriarPedido(modelPedido);

                //mensagem ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pela compra !";
                ViewBag.PedidoTotal = _carrinhoCompra.GetCarrinhoCompraTotal();

                //limpa o carrinho 
                _carrinhoCompra.LimparCarrinho();
                _clienteService.LimparSessaoCliente();

                //exibir a view com dados de cliente e pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", modelPedido);
            }
            return View(cliente);

        }

        public IActionResult AplicarCupom(string codigoCupom)
        {
            var cupom  = _context.Cupons.FirstOrDefault(c => c.CodigoCupom == codigoCupom);
            if(cupom != null)
            {
                decimal desconto = cupom.Valor;
            }



            return View();
        }

    }
}
