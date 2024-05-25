using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Dto;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;
using System.Text;

namespace SwitchSelect.Controllers
{
    public class TrocaController : Controller
    {
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly SwitchSelectContext _context;

        public TrocaController(IJogoRepositorio jogoRepositorio, SwitchSelectContext context)
        {
            _jogoRepositorio = jogoRepositorio;
            _context = context;
        }
        
        [HttpPost]
        public IActionResult SolicitarTrocaProduto([FromForm] TrocaProdutosRequest request, int pedidoId)
        {
            //filtar pedido
            var detalhesPedido = _context.PedidoDetalhes.Where(d => d.PedidoId == pedidoId).ToList();

            foreach (var jogoId in request.Quantidade.Keys)
            {
                var quantidade = request.Quantidade[jogoId];
                var motivo = request.Motivo[jogoId];

                //buscar dados do jogo para troca
                var jogoTroca = _jogoRepositorio.GetJogoPorId(jogoId);
                //atualizar restricao do pedido do produto
                var detalhe = detalhesPedido.FirstOrDefault(d => d.JogoId == jogoId);
                //buscar pedido
                var pedido = _context.Pedidos.Find(pedidoId);
                //buscar cliente
                var cliente = _context.Clientes.Find(pedido.ClienteId);

                if (detalhe != null)
                {
                    detalhe.Restricao = "EM TROCA";
                    _context.Update(detalhe);
                }
                //verifica se tem jogo para troca
                if (motivo != null)
                {
                    var trocaJogo = new TrocaProduto();
                    trocaJogo.Motivo = motivo;
                    trocaJogo.JogoId = jogoId;
                    trocaJogo.NomeJogo = detalhe.NomeJogo;
                    trocaJogo.PedidoId = pedido.Id;
                    trocaJogo.Status = "TROCA SOLICITADA";
                    trocaJogo.DataSolicitacao = DateTime.Now;
                    trocaJogo.Qtd = quantidade;
                    trocaJogo.Valor = jogoTroca.Preco * quantidade;
                    trocaJogo.ClienteId = cliente.Id;

                    _context.TrocaProdutos.Add(trocaJogo);
                    _context.SaveChanges();
                }
                
            }

            //mensagem após solicitar troca
            ViewBag.Titulo = "Troca Solicitada";
            ViewBag.Mensagem = "Sua troca foi solicitada. Acompanhe o andamento em seus pedidos.";

            return View("~/Views/Mensagem/Mensagem.cshtml");
        }

        //lista de solicitação de trocas de produto
        public IActionResult ListaSolicitacoesTroca()
        {
            var trocas = _context.TrocaProdutos.Include(c => c.Cliente).ToList();

            return View(trocas);
        }

        //negar troca de produto
        public IActionResult NegarTrocaProduto(int jogoId)
        {
            //buscando detalhes de produto para alterar restrição
            var detalhe = _context.PedidoDetalhes.FirstOrDefault(d => d.JogoId == jogoId);
            //buscando trocaProduto para alterar status
            var troca = _context.TrocaProdutos.FirstOrDefault(t => t.JogoId == jogoId);

            //lista de trocas solicitadas
            var trocas = _context.TrocaProdutos.Include(c => c.Cliente).ToList();

            if (detalhe != null && troca != null)
            {
                detalhe.Restricao = "Troca não aprovada. Entre em contato para mais informações.";
                troca.Status = "Troca não aprovada";

                _context.Update(troca);
                _context.Update(detalhe);
                _context.SaveChanges();
            }
            ViewBag.Titulo = "Troca não aprovada";
            ViewBag.Mensagem = "A troca não foi aprovada. O cliente será informado";
            return View("~/Views/Mensagem/Mensagem.cshtml");
        }

        public IActionResult AprovarTrocaProduto(int jogoId, int pedidoId)
        {
            // Buscar o detalhe do pedido específico pelo pedidoId e jogoId
            var detalhe = _context.PedidoDetalhes
                                  .Include(d => d.Pedido)
                                  .FirstOrDefault(d => d.PedidoId == pedidoId && d.JogoId == jogoId);


            // Buscar a troca específica relacionada ao pedidoId e jogoId
            var troca = _context.TrocaProdutos.FirstOrDefault(t => t.PedidoId == pedidoId && t.JogoId == jogoId);

            //lista de trocas solicitadas
            var trocas = _context.TrocaProdutos.Include(c => c.Cliente).ToList();

            if (detalhe != null && troca != null)
            {
                detalhe.Restricao = "Troca aprovada";
                troca.Status = "Aguardando envio";

                _context.Update(troca);
                _context.Update(detalhe);
                _context.SaveChanges();
            }
            ViewBag.Titulo = "Troca aprovada";
            ViewBag.Mensagem = "A troca foi aprovada. Aguardando envio do produto.";
            return View("~/Views/Mensagem/Mensagem.cshtml");
        }

        public IActionResult ConfirmarEnvioProduto(int pedidoId, int jogoId)
        {
            //buscar plista de troca
            var itensTroca = _context.TrocaProdutos.Where(i => i.PedidoId == pedidoId);
            //buscando detalhes de produto para alterar restrição
            var detalhe = _context.PedidoDetalhes.FirstOrDefault(d => d.JogoId == jogoId);

            foreach (var item in itensTroca)
            {
                if (item.JogoId == jogoId && detalhe.JogoId == jogoId)
                {
                    item.Status = "Prd. Enviado";
                    detalhe.Restricao = "Aguardando confirmação de recebimento do produto.";
                    _context.Update(item);

                }
            }

            ViewBag.Titulo = "Confirmação de envio";
            ViewBag.Mensagem = "Nossa equipe irá confirmar o recebimento do produto. Acompanhe o andamento em seu produtos";
            _context.SaveChanges();
            return View("~/Views/Mensagem/Mensagem.cshtml");
        }

        public IActionResult ProdutoNaoRecebido(int jogoId, int pedidoId)
        {
            try
            {
                //filtar pedido
                var detalhesPedido = _context.PedidoDetalhes.Where(d => d.PedidoId == pedidoId).ToList();

                //buscando detalhes de produto para alterar restrição
                var detalhe = detalhesPedido.FirstOrDefault(d => d.JogoId == jogoId);

                //lista de trocas solicitadas
                var trocas = _context.TrocaProdutos.Where(t => t.PedidoId == detalhe.PedidoId).Include(c => c.Cliente).ToList();

                //buscando trocaProduto para alterar status
                var troca = trocas.FirstOrDefault(t => t.JogoId == jogoId);

                if (detalhe != null && troca != null)
                {
                    detalhe.Restricao = "Troca cancelada. Não recebemos o produto em nossa loja.";
                    troca.Status = "Troca cancelada";

                    _context.Update(troca);
                    _context.Update(detalhe);
                    _context.SaveChanges();
                }
                ViewBag.Titulo = "Troca cancelada";
                ViewBag.Mensagem = "A troca foi cancelada. Produto não foi recebido em nossa loja.";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.Titulo = "Erro";
                ViewBag.Mensagem = "Ocorreu um erro ao processar a solicitação." + ex;
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
        }

        public IActionResult ProdutoRecebido(int jogoId, int pedidoId)
        {
            try
            {
                //filtar pedido
                var detalhesPedido = _context.PedidoDetalhes.Where(d => d.PedidoId == pedidoId).ToList();
                //buscando detalhes de produto para alterar restrição
                var detalhe = detalhesPedido.FirstOrDefault(d => d.JogoId == jogoId);
                //lista de trocas solicitadas
                var trocas = _context.TrocaProdutos.Where(t => t.PedidoId == detalhe.PedidoId).Include(c => c.Cliente).ToList();
                //buscando trocaProduto para alterar status
                var troca = trocas.FirstOrDefault(t => t.JogoId == jogoId);

                if (detalhe != null && troca != null)
                {
                    detalhe.Restricao = "Troca finalizada. O produto foi recebido pela nossa equipe, seu cupom de troca foi gerado.";
                    troca.Status = "Troca finalizada";

                    _context.Update(troca);
                    _context.Update(detalhe);
                    
                }

                //criando cupom de troca
                var cupom = new Cupom();
                //gerando código de cupom
                var codigo = new StringBuilder();
                codigo.Append("troca-");
                string valor = troca.Valor.ToString("C2");
                codigo.Append(valor);
                codigo.Append("-" + cupom.GerarCodigoCupom());
                cupom.CodigoCupom = codigo.ToString();
                //status de cupom
                cupom.Status = "Valido";
                cupom.Valor = troca.Valor;
                cupom.ClienteId = troca.ClienteId;
                _context.Add(cupom);
                //salvando todas as alterações no banco
                _context.SaveChanges();

                ViewBag.Titulo = "Troca finalizada";
                ViewBag.Mensagem = "A troca foi finalizada com sucesso. O cupom do cliente foi gerado e disponibilizado para o cliente.";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.Titulo = "Erro";
                ViewBag.Mensagem = "Ocorreu um erro ao processar a solicitação." + ex;
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
        }
    }
}
