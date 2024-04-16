using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Repositorios;

public class EnderecoRepositorio : IEnderecoRepositorio
{
    private readonly SwitchSelectContext _context;

    public EnderecoRepositorio(SwitchSelectContext context)
    {
        _context = context;
    }

    public IEnumerable<Endereco> Enderecos => _context.Enderecos
        .Include(e => e.Bairro)
            .ThenInclude(b => b.Cidade)
                .ThenInclude(c => c.Estado);

    public IEnumerable<Endereco> ObterEnderecosPorCliente(int clienteId)
    {
        return _context.Enderecos
            .Where(e => e.ClienteId == clienteId)
            .Include(e => e.Bairro)
                .ThenInclude(b => b.Cidade)
                    .ThenInclude(c => c.Estado)
            .ToList();
    }

    public Endereco? GetEnderecoPorId(int enderecoId)
    {
        return _context.Enderecos
            .Include(e => e.Bairro)
                .ThenInclude(b => b.Cidade)
                    .ThenInclude(c => c.Estado)
            .FirstOrDefault(e => e.Id == enderecoId);
    }
}
