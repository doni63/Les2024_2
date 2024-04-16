using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Repositorios;

public class JogoRepositorio : IJogoRepositorio
{
    private readonly SwitchSelectContext _context;

    public JogoRepositorio(SwitchSelectContext context)
    {
        _context = context;
    }

    public IEnumerable<Jogo> Jogos => _context.Jogos.Include(c => c.Categoria);

    public IEnumerable<Jogo> JogosPreferidos => _context.Jogos
                              .Where(j => j.IsJogoPreferido == true)
                              .Include(c => c.Categoria);

    public Jogo GetJogoPorId(int id)
    {
        return _context.Jogos.FirstOrDefault(j => j.Id == id);
    }

    
}
