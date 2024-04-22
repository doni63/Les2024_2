using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;


namespace SwitchSelect.Models;

public class CarrinhoCompra
{
    private readonly SwitchSelectContext _context;

    public CarrinhoCompra(SwitchSelectContext context)
    {
        _context = context;
    }
    //teste
    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhosCompraItens { get; set; }
    public int QuantidadeProdutosTotal => CarrinhosCompraItens?.Sum(item => item.Quantidade) ?? 0;

    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        //definir uma sessão
        ISession session =
            services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        //obter serviço do nosso contexto
        var context = services.GetService<SwitchSelectContext>();

        //obtem ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        //atribui o id do carrinho na sessão
        session.SetString("CarrinhoId", carrinhoId);

        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }
    
    public void AdicionarAoCarrinho(Jogo jogo)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens
            .SingleOrDefault(s => s.Jogo.Id == jogo.Id &&
            s.CarrinhoCompraId == CarrinhoCompraId
            );

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Jogo = jogo,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
           
        }
        _context.SaveChanges();
    }

    public int RemoverDoCarrinho(Jogo jogo)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens
           .SingleOrDefault(s => s.Jogo.Id == jogo.Id &&
           s.CarrinhoCompraId == CarrinhoCompraId
           );
        var quantidadeLocal = 0;
        if (carrinhoCompraItem != null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }
        }
        _context.SaveChanges();
        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhosCompraItens()
    {
        return CarrinhosCompraItens??
            (CarrinhosCompraItens =
            _context.CarrinhoCompraItens
            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
            .Include(j => j.Jogo).ToList());
    }

    public void LimparCarrinho()
    {
        var carriinhoItens = _context.CarrinhoCompraItens
            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

        _context.RemoveRange(carriinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.CarrinhoCompraItens
            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
            .Select(c => c.Jogo.Preco * c.Quantidade).Sum();

        return total;
    }

    public void AplicarDesconto(decimal valorDesconto, List<CarrinhoCompraItem> carrinhoCompraItens)
    {
        decimal valorTotal = GetCarrinhoCompraTotal();
        decimal novoValorTotal = valorTotal - valorDesconto;

        // Verifica se o desconto não deixa o valor total negativo
        if (novoValorTotal >= 0)
        {
            // Atualiza o valor total do carrinho com o desconto aplicado
            foreach (var item in carrinhoCompraItens)
            {
                item.Jogo.Preco -= valorDesconto / item.Quantidade;
            }
        }
        else
        {
            // Se o desconto for maior que o valor total do carrinho, define o valor total como 0
            foreach (var item in carrinhoCompraItens)
            {
                item.Jogo.Preco = 0;
            }
        }
    }
}
