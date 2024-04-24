using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Repositorios;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly SwitchSelectContext _context;

    public ClienteRepositorio(SwitchSelectContext context)
    {
        _context = context;
    }

    public IEnumerable<Cliente> Clientes => _context.Clientes
        .Include(c => c.Telefones)
        .Include(c => c.Cartoes)
        .Include(c => c.Enderecos)
            .ThenInclude(e => e.Bairro)
            .ThenInclude(b => b.Cidade)
            .ThenInclude(c => c.Estado)
            .ThenInclude(p => p.Pais);

    public Cliente? GetPorCpf(string cpf)
    {
        return _context.Clientes
            .Include(c => c.Telefones)
            .Include(c => c.Cartoes)
            .Include(c => c.Enderecos)
                .ThenInclude(e => e.Bairro)
                .ThenInclude(b => b.Cidade)
                .ThenInclude(c => c.Estado)
                .ThenInclude(p => p.Pais)

                .FirstOrDefault(c => c.Cpf == cpf);
    }

    public Cliente? GetPorId(int id)
    {
        return _context.Clientes
            .Include(c => c.Telefones)
            .Include(c => c.Cartoes)
            .Include(c => c.Enderecos)
                .ThenInclude(e => e.Bairro)
                .ThenInclude(b => b.Cidade)
                .ThenInclude(c => c.Estado)
                .ThenInclude(p => p.Pais)

            .FirstOrDefault(c => c.Id == id);
    }

}
