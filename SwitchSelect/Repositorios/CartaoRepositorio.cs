using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Repositorios;

public class CartaoRepositorio : ICartaoRepositorio
{
    private readonly SwitchSelectContext _context;
    public CartaoRepositorio(SwitchSelectContext context)
    {
        _context = context;
    }
    public IEnumerable<Cartao> Cartoes => _context.Cartoes;
        

    public Cartao? GetCartaoPorId(int cartaoId)
    {
        return _context.Cartoes
            .FirstOrDefault(c => c.Id == cartaoId);
    }

    public IEnumerable<Cartao> ObterCartaoPorCliente(int clienteId)
    {
        return _context.Cartoes
            .Where(c => c.ClienteId == clienteId).ToList();
            
    }
}
