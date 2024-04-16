using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.Carrinho;


namespace SwitchSelect.Service;

public class CarrinhoService
{
    private readonly SwitchSelectContext _context;

    public CarrinhoService(SwitchSelectContext context)
    {
        _context = context;
    }
    public string  CarrinhoCompraId { get; set; }
    
    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        //definir uma sessão
        ISession session =
            services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        //obter serviço do nosso contexto
        var context = services.GetService<SwitchSelectContext>();

        //obtem ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId")?? Guid.NewGuid().ToString();

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

    
}
