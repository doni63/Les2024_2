using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Service
{
    public class AdminService
    {
        private readonly SwitchSelectContext _context;
        private readonly CartaoService _cartaoService;

        public AdminService(SwitchSelectContext context, CartaoService cartaoService)
        {
            _context = context;
            _cartaoService = cartaoService;
        }

        public List<Cliente> AdminListarClientes()
        {
            return _context.Clientes.ToList();
        }

        public async Task<Cliente> AdminGetCliente(int id)
        {
            if (id == null) return null;

            var cliente = await _context.Clientes
                 .Include(c => c.Telefones)
                 .Include(c => c.Enderecos)
                     .ThenInclude(e => e.Bairro)
                     .ThenInclude(b => b.Cidade)
                     .ThenInclude(c => c.Estado)
                .Include(c => c.Cartoes)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null) return null;
            foreach(var cartao in cliente.Cartoes)
            {
               var numeroCartao = cartao.NumeroCartao;
               cartao.CartaoQuatroDigito = _cartaoService.FormatarUltimosQuatroDigitos(numeroCartao);
            }

            return cliente;
        }

        public List<Cliente> AdminPesquisarCliente(string pesquisa)
        {
            var clientes = _context.Clientes.AsQueryable();

            if(! String.IsNullOrEmpty(pesquisa))
            {
                clientes = clientes.Where(c => c.Nome.Contains(pesquisa) 
                || c.Cpf.Contains(pesquisa));
            }
            return clientes.ToList();
        }

    }
}
