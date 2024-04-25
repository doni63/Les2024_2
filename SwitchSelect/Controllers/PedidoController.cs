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


        public PedidoController(IPedidoRepositorio pedidoRepositorio, CarrinhoCompra carrinhoCompra, SwitchSelectContext context, CartaoService cartaoService, ClienteService clienteService, IClienteRepositorio clienteRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _carrinhoCompra = carrinhoCompra;
            _context = context;
            _cartaoService = cartaoService;
            _clienteRepositorio = clienteRepositorio;

        }
        public IActionResult CheckoutCpf()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckoutCpf(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                //string cpf = model.Cpf;

                if (model.Cpf != null)
                {
                    var cliente = _clienteRepositorio.GetPorCpf(model.Cpf);
                    foreach(var cartao in cliente.Cartoes)
                    {
                        var numeroCartao = cartao.NumeroCartao;
                        cartao.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);
                    }
                    return View("Checkout", cliente);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "CPF não encontrado");
                }
            }
            return View(model);
        }
        public IActionResult Checkout(Cliente cliente)
        {
            return View(cliente);
        }

        //[HttpGet]
        //public IActionResult Checkout()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Checkout(ClientePedidoViewModel pedido)
        //{
        //    int totalItensPedido = 0;
        //    decimal precoTotalPedido = 0.0m;

        //    //obter itens do carrinho de compra do cliente
        //    List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhosCompraItens();
        //    _carrinhoCompra.CarrinhosCompraItens = itens;

        //    if (_carrinhoCompra.CarrinhosCompraItens.Count == 0)
        //    {
        //        ModelState.AddModelError("", "Carrinho vazio");
        //    }

        //    //calcular total de pedidos e de itens
        //    foreach (var item in itens)
        //    {
        //        totalItensPedido += item.Quantidade;
        //        precoTotalPedido += (item.Jogo.Preco * item.Quantidade);
        //    }

        //    var modelPedido = new Pedido();

        //    //dados Cliente
        //    var cliente = new Cliente
        //    {
        //        Nome = pedido.Nome,
        //        RG = pedido.RG,
        //        Cpf = pedido.Cpf,
        //        Genero = pedido.Genero,
        //        DataDeNascimento = pedido.DataDeNascimento,
        //        Email = pedido.Email
        //    };  

        //    //dados de telefone
        //    var telefone = new Telefone
        //    {
        //        TipoTelefone = pedido.TipoTelefone,
        //        DDD = pedido.DDD,
        //        NumeroTelefone = pedido.NumeroTelefone,
        //        Cliente = cliente
        //    };

        //    //dados de endereco
        //    var pais = new Pais
        //    {
        //        Descricao = "Brasil"
        //    };
        //    var estado = new Estado
        //    {
        //        Descricao = pedido.Estado,
        //        Pais = pais
        //    };

        //    var cidade = new Cidade
        //    {
        //        Descricao = pedido.Cidade,
        //        Estado = estado
        //    };

        //    var bairro = new Bairro
        //    {
        //        Descricao = pedido.Bairro,
        //        Cidade = cidade
        //    };

        //    var endereco = new Endereco
        //    {
        //        TipoEndereco = pedido.TipoEndereco,
        //        TipoLogradouro = pedido.TipoLogradouro,
        //        Logradouro = pedido.Logradouro,
        //        Numero = pedido.Numero,
        //        CEP = pedido.CEP,
        //        TipoResidencia = pedido.TipoResidencia,
        //        Complemento = pedido.Complemento,
        //        Cliente = cliente,
        //        Bairro = bairro
        //    };
        //    //dados de cartao
        //    var cartao = new Cartao
        //    {
        //        NumeroCartao = pedido.NumeroCartao,
        //        CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(pedido.NumeroCartao),
        //        Bandeira = pedido.Bandeira,
        //        TitularDoCartao = pedido.TitularDoCartao,
        //        CpfTitularCartao = pedido.CpfTitularCartao,
        //        DataValidade = new DateTime(pedido.AnoValidade, pedido.MesValidade, DateTime.DaysInMonth(pedido.AnoValidade, pedido.MesValidade)),
        //        CVV = pedido.CVV,
        //        TipoCartao = pedido.TipoCartao,
        //        Cliente = cliente
        //    };

        //    cliente.Telefones.Add(telefone);
        //    cliente.Enderecos.Add(endereco);
        //    cliente.Cartoes.Add(cartao);
        //    _context.Clientes.Add(cliente);
        //    _context.SaveChanges();

        //    //dados Pedido
        //    modelPedido.ClienteId = cliente.Id;
        //    modelPedido.Cliente = cliente;
        //    modelPedido.Telefone = telefone;
        //    modelPedido.TelefoneId = telefone.Id;
        //    modelPedido.EnderecoId = endereco.Id;
        //    modelPedido.Endereco = endereco;
        //    modelPedido.Cartao = cartao;
        //    modelPedido.cartaoId = cartao.Id;
        //    modelPedido.Status = "Processando";
        //    modelPedido.TotalItensPedido = totalItensPedido;
        //    modelPedido.PedidoTotal = precoTotalPedido;

        //    //valida os dados do pedido
        //    if (ModelState.IsValid)
        //    {
        //        //cria os pedidos e os detalhes

        //        _pedidoRepositorio.CriarPedido(modelPedido);

        //        //mensagem ao cliente
        //        ViewBag.CheckoutCompletoMensagem = "Obrigado pela compra !";
        //        ViewBag.PedidoTotal = _carrinhoCompra.GetCarrinhoCompraTotal();

        //        //limpa o carrinho
        //        _carrinhoCompra.LimparCarrinho();

        //        //exibir a view com dados de cliente e pedido
        //        return View("~/Views/Pedido/CheckoutCompleto.cshtml", modelPedido);
        //    }
        //    return View(pedido);

        //}

    }
}
