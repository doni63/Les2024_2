using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;



namespace SwitchSelect.Data;

public class SwitchSelectContext : /*DbContext*/ IdentityDbContext<IdentityUser>
{
    public SwitchSelectContext(DbContextOptions<SwitchSelectContext> options)
        : base(options)
    {

    }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Estoque> Estoques { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Bairro> Bairros { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Cartao> Cartoes {  get; set; } 
    public DbSet<Telefone> Telefones {  get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Cupom> Cupons { get; set; }
    public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
    public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<PagamentoCartao> PagamentosCartoes { get; set; }
    public DbSet<TrocaProduto> TrocaProdutos { get; set; }

    public DbSet<ClienteCompletoViewModel> ClienteViewModels { get; set; } = default!;

    public DbSet<EnderecoViewModel> EnderecoViewModel { get; set; } = default!;

    public DbSet<ClienteDadosPessoaisViewModel> ClienteDadosPessoaisViewModel { get; set; } = default!;

    public DbSet<CartaoViewModel> CartaoViewModel { get; set; } = default!;
    

    

}
