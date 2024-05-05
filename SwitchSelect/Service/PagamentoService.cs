using SwitchSelect.Data;
using SwitchSelect.Dto;
using SwitchSelect.Models;

namespace SwitchSelect.Service;

public class PagamentoService
{
    private readonly SwitchSelectContext _context;

    public PagamentoService(SwitchSelectContext context)
    {
        _context = context;
    }

    public Pagamento CriarPagamento(CartaoIdValor cartaoIdValor)
    {
        var pagamento = new Pagamento
        {
            Valor = cartaoIdValor.Valor,
            Tipo = "Cartao",
            
        };
        pagamento.CartaoIds.Add(cartaoIdValor.Id);

        _context.Pagamentos.Add(pagamento);
        _context.SaveChanges();

        return pagamento;
    }

    public PagamentoCartao CriarPagamentoCartao(int pagamentoId, int cartaoId)
    {
        // Verifique se o pagamento e o cartão existem no banco de dados
        var pagamento = _context.Pagamentos.FirstOrDefault(p => p.Id == pagamentoId);
        var cartao = _context.Cartoes.FirstOrDefault(c => c.Id == cartaoId);

        if (pagamento == null || cartao == null)
        {
            // Se o pagamento ou o cartão não existirem, retorne null ou lance uma exceção, dependendo do seu caso de uso.
            return null;
        }

        // Crie uma nova instância de PagamentoCartao e associe o Pagamento e o Cartao a ela
        var pagamentoCartao = new PagamentoCartao
        {
            PagamentoId = pagamentoId,
            Pagamento = pagamento,
            CartaoId = cartaoId,
            Cartao = cartao
        };

        // Adicione o PagamentoCartao ao contexto e salve as alterações
        _context.PagamentosCartoes.Add(pagamentoCartao);
        _context.SaveChanges();

        // Retorne o PagamentoCartao recém-criado
        return pagamentoCartao;
    }

}
