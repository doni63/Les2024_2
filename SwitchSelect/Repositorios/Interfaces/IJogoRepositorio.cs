using SwitchSelect.Models;

namespace SwitchSelect.Repositorios.Interfaces;

public interface IJogoRepositorio
{
    IEnumerable<Jogo> Jogos { get; }
    IEnumerable<Jogo> JogosPreferidos { get; }
    Jogo GetJogoPorId(int id);

}
