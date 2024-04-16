using SwitchSelect.Models;

namespace SwitchSelect.Repositorios.Interfaces;

public interface IClienteRepositorio
{
    IEnumerable<Cliente> Clientes {  get; }
    Cliente GetPorId(int id);
}
