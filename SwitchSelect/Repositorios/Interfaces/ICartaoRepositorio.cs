using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;

namespace SwitchSelect.Repositorios.Interfaces;

public interface ICartaoRepositorio
{
    IEnumerable<Cartao> Cartoes { get; }
    IEnumerable<Cartao> ObterCartaoPorCliente(int clienteId);
    Cartao GetCartaoPorId(int id);
   
}
