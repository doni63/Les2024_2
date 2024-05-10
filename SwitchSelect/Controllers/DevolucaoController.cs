using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using System.Diagnostics;

namespace SwitchSelect.Controllers
{
    public class DevolucaoController : Controller
    {
        private readonly SwitchSelectContext _context;

        public DevolucaoController(SwitchSelectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Devolucao(int pedidoId)
        {
            //buscar pedido
            var pedido = _context.Pedidos.Find(pedidoId);
            var devolucao = new Devolucao();

            if (pedido != null)
            {
                devolucao.Status = pedido.Status;
                devolucao.ClienteId = pedido.ClienteId;
                devolucao.PedidoId = pedido.Id;
                devolucao.DataSolicitacao = DateTime.Now;
                devolucao.Motivo = "";
                devolucao.Valor = pedido.PedidoTotal;
            }

            return View(devolucao);
        }

        [HttpPost]
        public IActionResult Devolucao([FromForm] Devolucao devolucao)
        {


            if (ModelState.IsValid)
            {
                devolucao.Status = "Em devolução";
                //atualizanod status em pedido
                var pedido = _context.Pedidos.Find(devolucao.PedidoId);
                pedido.Status = "Em devolução";


                _context.Update(pedido);
                _context.Devolucoes.Add(devolucao);
                _context.SaveChanges();

                ViewBag.Titulo = "Devolução Solicitada";
                ViewBag.Mensagem = "Seu  solicitação de devolução foi registrada. Envie seu produto(s) à nossa" +
                    "loja para que a equipe de continuidade a sua solicitação";

                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
            else
            {
                return View(devolucao);
            }


        }

        public IActionResult ListaDevolucoes()
        {
            var devolucoes = _context.Devolucoes.Include(c => c.Cliente).ToList();
            return View(devolucoes);
        }

        public IActionResult ConfirmarEnvio(int pedidoId)
        {
            //buscar pedido e atualiza status
            var pedido = _context.Pedidos.Find(pedidoId);
            //buscar devolucao para atualizar status
            var devolucao = _context.Devolucoes.FirstOrDefault(d => d.PedidoId == pedidoId);
            //buscar pedido detalhes para atualizar restrição
            var detalhe = _context.PedidoDetalhes.FirstOrDefault(d => d.PedidoId == pedidoId);

            if (pedido != null && devolucao != null)
            {
                detalhe.Restricao = "Aguardando confirmação de recebimento";
                pedido.Status = "Pedido enviado";
                devolucao.Status = "Pedido enviado";

                _context.Update(detalhe);
                _context.Update(pedido);
                _context.Update(devolucao);
                _context.SaveChanges();

                ViewBag.Titulo = "Aguardando confirmação";
                ViewBag.Mensagem = "Assim que nossa equipe identificar a devolução da mercadoria seu cupom com valor do pedido será disponibilizado.";

                return View("~/Views/Mensagem/Mensagem.cshtml");

            }
            else
            {
                ViewBag.Titulo = "Erro";
                ViewBag.Mensagem = "Não foi possível efetuar sua solicitação";

                return View("~/Views/Mensagem/Mensagem.cshtml");
            }

        }

        public IActionResult ConfirmarDevolucao(int pedidoId)
        {
            //buscar pedido
            var pedido = _context.Pedidos.Find(pedidoId);
            //buscar devolucao
            var devolucao = _context.Devolucoes.FirstOrDefault(d => d.PedidoId == pedidoId);
            //buscar detalhe
            var detalhe = _context.PedidoDetalhes.FirstOrDefault(d => d.PedidoId == pedidoId);

            if (pedido != null && detalhe != null && devolucao != null)
            {
                pedido.Status = "Devolucao finalizada";
                devolucao.Status = "Devolucao finalizada";
                detalhe.Restricao = "Cupom de devolução disponível";

                _context.Update(pedido);
                _context.Update(devolucao);
                _context.Update(detalhe);
                

                //criando cupom de troca
                var cupom = new Cupom();
                //gerando código de cupom
                cupom.CodigoCupom = cupom.GerarCodigoCupom();
                //status de cupom
                cupom.Status = "Valido";
                cupom.Valor = devolucao.Valor;
                cupom.ClienteId = pedido.ClienteId;
                _context.Add(cupom);

                //salvando todas as alterações no banco
                _context.SaveChanges();

                ViewBag.Titulo = "Devolução finalizada";
                ViewBag.Mensagem = "A devolução foi finalizada com sucesso. O cupom do cliente foi gerado e disponibilizado para o cliente.";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
            else
            {
                ViewBag.Titulo = "Erro";
                ViewBag.Mensagem = "Não foi possível finalizar a devolução";
                return View("~/Views/Mensagem/Mensagem.cshtml");
            }
           
        }
    }
}
