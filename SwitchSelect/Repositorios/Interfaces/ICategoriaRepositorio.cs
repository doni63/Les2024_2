using SwitchSelect.Models;

namespace SwitchSelect.Repositorios.Interfaces;

public interface ICategoriaRepositorio
{
    IEnumerable<Categoria> Categorias { get; }
}
