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
        private readonly ICartaoRepositorio _cartaoRepositorio;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly SwitchSelectContext _context;
        private readonly CartaoService _cartaoService;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ClienteService _clienteService;


        public PedidoController(IPedidoRepositorio pedidoRepositorio,ICartaoRepositorio cartaoRepositorio, CarrinhoCompra carrinhoCompra, SwitchSelectContext context, CartaoService cartaoService, ClienteService clienteService, IClienteRepositorio clienteRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _cartaoRepositorio = cartaoRepositorio;
            _carrinhoCompra = carrinhoCompra;
            _context = context;
            _cartaoService = cartaoService;
            _clienteRepositorio = clienteRepositorio;
            _clienteService = clienteService;

        }
        [HttpGet]
        public IActionResult CheckoutLogin(int quantidade)
        {
            if(quantidade > 1)
            {
                return View("~/Views/Pedido/PedidoError.Cshtml");
            }
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
            return View("~/Views/Pedido/PedidoError.Cshtml");
        }



        [HttpPost]
        public IActionResult Checkout(int enderecoId, List<int> cartoesSelecionados, decimal precoTotalPedido, int totalItensPedido, string cupomAplicado)
        {
            // Buscar cliente da sessão
            var cliente = _clienteService.ObterClienteDaSessao();

            // Obter itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhosCompraItens();

            // Valor total do pedido
            decimal totalValorPedido = itens.Sum(item => item.Jogo.Preco * item.Quantidade);

            // Criar lista de pagamentos
            var pagamentos = new List<Pagamento>();

            // Adicionar os cartões selecionados aos pagamentos
            if (cartoesSelecionados != null && cartoesSelecionados.Any())
            {
                foreach (var cartaoId in cartoesSelecionados)
                {
                    var cartao = _cartaoRepositorio.GetCartaoPorId(cartaoId);
                    if (cartao != null)
                    {
                        var pagamento = new Pagamento
                        {
                            Valor = precoTotalPedido,
                            Tipo = "Cartão de Crédito",
                            NumerosCartao = new List<string> { cartao.NumeroCartao }
                        };
                        pagamentos.Add(pagamento);
                    }
                }
            }

            // Criar um novo pedido
            var modelPedido = new Pedido
            {
                ClienteId = cliente.Id,
                Nome = cliente.Nome,
                TelefoneId = cliente.Telefones.FirstOrDefault().Id,
                EnderecoId = enderecoId,
                Status = "Processando",
                TotalItensPedido = totalItensPedido,
                PedidoTotal = precoTotalPedido,
                Pagamentos = pagamentos // Adicionar os pagamentos ao pedido
            };

            // Aplicar desconto se houver cupom aplicado
            decimal desconto = 0m;
            if (!string.IsNullOrEmpty(cupomAplicado))
            {
                var cupom = _context.Cupons.FirstOrDefault(c => c.CodigoCupom == cupomAplicado);
                if (cupom != null)
                {
                    cupom.Status = "Usado";
                    cupom.ClienteId = cliente.Id;
                    _context.Update(cupom);
                    _context.SaveChanges();
                    desconto = totalValorPedido - precoTotalPedido;
                }
            }

            // Validar os dados do pedido
            ModelState.Remove("cupomAplicado");
            if (ModelState.IsValid)
            {
                // Criar o pedido e os detalhes
                _pedidoRepositorio.CriarPedido(modelPedido);

                // Limpar o carrinho e a sessão do cliente
                _carrinhoCompra.LimparCarrinho();
                _clienteService.LimparSessaoCliente();

                // Exibir a view com os dados do pedido
                ViewBag.CheckoutCompletoMensagem = "Obrigado pela compra!";
                ViewBag.PedidoTotal = precoTotalPedido;
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", modelPedido);
            }

            // Retornar a view de erro em caso de problemas com a validação dos dados do pedido
            return View("~/Views/Pedido/PedidoError.cshtml");
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



        public async Task<IActionResult> PedidoListCliente(int clienteId)
        {
            var pedidoCliente = await _context.Pedidos
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
            return View(pedidoCliente);
        }

        public IActionResult PedidoDetalhe(int pedidoId, string pedidoTotal)
        {
            var itensPedido = _context.PedidoDetalhes.Where(pd => pd.PedidoId == pedidoId).ToList();

            ViewBag.PedidoTotal = pedidoTotal;
            return View(itensPedido);
        }
    }
}
