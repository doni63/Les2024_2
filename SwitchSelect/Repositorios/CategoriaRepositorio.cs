using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Repositorios
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly SwitchSelectContext _context;
        public CategoriaRepositorio(SwitchSelectContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
