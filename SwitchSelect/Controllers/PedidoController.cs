using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using SwitchSelect.Data;
using SwitchSelect.Dto;
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
        private readonly PagamentoService _pagamentoService;


        public PedidoController(IPedidoRepositorio pedidoRepositorio, CarrinhoCompra carrinhoCompra, SwitchSelectContext context, CartaoService cartaoService, ClienteService clienteService, IClienteRepositorio clienteRepositorio, PagamentoService pagamentoService)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _carrinhoCompra = carrinhoCompra;
            _context = context;
            _cartaoService = cartaoService;
            _clienteRepositorio = clienteRepositorio;
            _clienteService = clienteService;
            _pagamentoService = pagamentoService;

        }
        [HttpGet]
        public IActionResult CheckoutLogin(int quantidade)
        {
            if (quantidade > 1)
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
                var precoTotalPedidoBytes = BitConverter.GetBytes(Convert.ToDouble(precoTotalPedido));
                var totalItensPedidoBytes = BitConverter.GetBytes(totalItensPedido);

                HttpContext.Session.Set("PrecoTotalPedido", precoTotalPedidoBytes);
                HttpContext.Session.Set("TotalItensPedido", totalItensPedidoBytes);

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
        public IActionResult Checkout(int enderecoId, string cartoesIds, decimal total, int totalItensPedido, string cupomAplicado)
        {
            //recuperar valor de frete da sessão para acrescentar no pedidoTotal
            var valorFreteString = HttpContext.Session.GetString("ValorFrete");
            decimal valorFrete = 0m;
            if (valorFreteString != null)
            {
                valorFrete = decimal.Parse(valorFreteString);
            }

            // recuperar dados do cartao do forms
            var cartoesIdsJson = JsonConvert.DeserializeObject<List<CartaoIdValor>>(cartoesIds);

            var pagamentos = new List<Pagamento>();

            // criando pagamento para cada cartao
            foreach (var cartaoIdValor in cartoesIdsJson)
            {
                var pagamento = _pagamentoService.CriarPagamento(cartaoIdValor);
                pagamentos.Add(pagamento);

                _pagamentoService.CriarPagamentoCartao(pagamento.Id, cartaoIdValor.Id);
            }

            //obter itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhosCompraItens();

            // Buscar cliente da sessão
            var cliente = _clienteService.ObterClienteDaSessao();

            // Crie um novo pedido
            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                Nome = cliente.Nome,
                TelefoneId = (int)cliente.Telefones.FirstOrDefault()?.Id,
                EnderecoId = enderecoId,
                Pagamentos = pagamentos,
                Status = "Processando",
                TotalItensPedido = totalItensPedido,
                PedidoTotal = total

            };

            // Se um cupom foi aplicado, atualize as informações do cupom
            if (!string.IsNullOrEmpty(cupomAplicado))
            {
                var cupom = _context.Cupons.FirstOrDefault(c => c.CodigoCupom == cupomAplicado);
                if (cupom != null)
                {
                    cupom.Status = "Usado";
                    cupom.ClienteId = cliente.Id;
                    _context.SaveChanges();
                    pedido.Desconto = cupom.Valor;
                }
            }
            ModelState.Remove("cupomAplicado");
            // Verifique a validade do modelo antes de criar o pedido
            if (ModelState.IsValid)
            {
                _pedidoRepositorio.CriarPedido(pedido);

                //mensagem ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pela compra. Estamos verificando o pagamento.";
                ViewBag.Frete = valorFrete;
                ViewBag.PedidoTotal = _carrinhoCompra.GetCarrinhoCompraTotal();
                ViewBag.Total = total;
                ViewBag.Desconto = pedido.Desconto;

                // Limpe o carrinho e a sessão do cliente
                _carrinhoCompra.LimparCarrinho();
                _clienteService.LimparSessaoCliente();

                // Exiba a view de checkout completo com os detalhes do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            else
            {
                // Se houver erros de validação, retorne a view de erro de pedido
                return View("~/Views/Pedido/PedidoError.cshtml");
            }
        }


        public IActionResult AplicarCupom(string Cpf, string codigoCupom, decimal total)
        {
            var cliente = _clienteRepositorio.GetPorCpf(Cpf);
            // Verifica se o valor total já é zero
           
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
            decimal precoTotalPedido = total;

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
                //precoTotalPedido += (item.Jogo.Preco * item.Quantidade);
            }
            //recuperar valor de frete da sessão para acrescentar no pedidoTotal
            var valorFreteString = HttpContext.Session.GetString("ValorFrete");
            decimal valorFrete = 0m;
            //recuperar valor do cep para buscar enderecoId
            var cep = HttpContext.Session.GetString("CepSession");
            var enderecoSelecionado = _context.Enderecos.FirstOrDefault(e => e.CEP == cep);
           
            if (valorFreteString != null)
            {
                 valorFrete = decimal.Parse(valorFreteString);
                //remove valor do frete da sessão
                HttpContext.Session.Remove("ValorFrete");
            }
            //adiciona o valor do frete no precoTotalPEdido
            precoTotalPedido += valorFrete;
            //aplica desconto
            precoTotalPedido -= desconto;
            
            if (precoTotalPedido < 0)
            {

                var valorTroco = precoTotalPedido * (-1);
                cupom.Status = "Usado";
                _context.Cupons.Update(cupom);

                var troco = new Cupom();
                troco.CodigoCupom = troco.GerarCodigoCupom();
                troco.Valor = valorTroco;
                troco.Status = "Valido";
                troco.ClienteId = cliente.Id;
                _context.Cupons.Add(troco);
                _context.SaveChanges();

                precoTotalPedido = 0;

                ViewBag.MensagemTroco = "Seu cupom de troco foi gerado no valor de R$" + valorTroco.ToString("c2");
            }
           
            // Verifica se o valor total após o desconto é menor que 10
            bool pagamentoAbaixoDezReaisPermitido = precoTotalPedido < 10;

            ViewBag.PrecoTotalPedido = precoTotalPedido;
            ViewBag.TotalItensPedido = totalItensPedido;
            ViewBag.PagamentoAbaixoDezReaisPermitido = pagamentoAbaixoDezReaisPermitido ? "True" : "False";
            ViewBag.EnderecoSelecionado = enderecoSelecionado;

           

            if (Cpf != null)
            {

                foreach (var cartao in cliente.Cartoes)
                {
                    var numeroCartao = cartao.NumeroCartao;
                    cartao.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);
                }
                //colocar cliente na sessao
                _clienteService.GuardarClienteNaSessao(cliente);

                //se cupom for inválido retorna a checkout
                if (cupom == null)
                {
                    ViewBag.MensagemErroCupom = "Cupom inválido";
                    return View("Checkout", cliente);
                }

                return View("CheckoutCupomAplicado", cliente);
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

        public IActionResult SelecionarPedidoTroca(int pedidoId, string pedidoTotal, string status)
        {
            var itensPedido = _context.PedidoDetalhes.Where(pd => pd.PedidoId == pedidoId).ToList();

            ViewBag.StatusPedido = status;
            ViewBag.PedidoTotal = pedidoTotal;
            ViewBag.PedidoId = pedidoId;
            return View(itensPedido);
        }

        public IActionResult ListaPagamentos()
        {
            var pagPedidos = _context.Pagamentos.ToList();

            return View("~/Views/Admin/ListaPagamentos.cshtml", pagPedidos);
        }

        public IActionResult AprovarPagamento(int pedidoId, int id)
        {
            //buscar pagamento com pedidoId
            var pagamento = _context.Pagamentos.Find(id);
            //buscar pedido para atualizar status
            var pedido = _context.Pedidos.Find(pedidoId);

            //verificar se existe pagamento
            if (pagamento != null)
            {
                pagamento.StatusPagamento = "Aprovado";
                _context.Update(pagamento);
            }
            else
            {
                ViewBag.Titulo = "Erro";
                ViewBag.Mensagem = "Esse pagamento não foi localizado";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }

            //verificar se existe pedido
            if (pedido != null)
            {
                pedido.Status = "Aprovado";
                _context.Update(pedido);
            }
            else
            {
                ViewBag.Titulo = "Erro";
                ViewBag.Mensagem = "Não foi encontrado um pedido vinculado a este pagamento";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }

            _context.SaveChanges();
            return RedirectToAction("ListaPagamentos", "Pedido");
        }
    }
}
