using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Components;

public class CategoriaMenu : ViewComponent
{
    private readonly ICategoriaRepositorio _categoriaRepositorio;

    public CategoriaMenu(ICategoriaRepositorio categoriaRepositorio)
    {
        _categoriaRepositorio = categoriaRepositorio;
    }

    public IViewComponentResult Invoke()
    {
        var categorias = _categoriaRepositorio.Categorias.OrderBy(c => c.Nome);

        return View(categorias);
    }
}
