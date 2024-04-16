using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;

namespace SwitchSelect.Controllers
{
    public class JogoController : Controller
    {
        private readonly IJogoRepositorio _jogoRepositorio;

        public JogoController(IJogoRepositorio jogoRepositorio)
        {
            _jogoRepositorio = jogoRepositorio;
        }

        public IActionResult JogoList(string categoria)
        {
            IEnumerable<Jogo> jogos;
            string categoriaAtual = string.Empty;

            jogos = _jogoRepositorio.Jogos
                    .Where(j => j.Categoria.Nome.Equals(categoria))
                    .OrderBy(j => j.Nome);

            if (jogos.Count() == 0)
            {
                jogos = _jogoRepositorio.Jogos.OrderBy(j => j.Id);
                categoriaAtual = "Todos os Jogos";
            }
            else
            {
                categoriaAtual = categoria;
            }
            var jogoListViewModel = new JogoListViewModel
            {
                Jogos = jogos,
                CategoriaAtual = categoriaAtual
            };
            return View(jogoListViewModel);
        }

        public IActionResult JogosPreferidos()
        {
            var jogosPreferidos = new JogoPreferidoViewModel();
            jogosPreferidos.JogosPreferidos = _jogoRepositorio.JogosPreferidos;

            return View(jogosPreferidos);
        }

        public IActionResult JogoSelecionado()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var jogo = _jogoRepositorio.Jogos.FirstOrDefault(c => c.Id == id);

            return View(jogo);
        }

        public ViewResult Pesquisa(string stringPesquisa)
        {
            IEnumerable<Jogo> jogos;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(stringPesquisa))
            {
                jogos = _jogoRepositorio.Jogos.OrderBy(j => j.Id);
                
            }
            else
            {
                jogos = _jogoRepositorio.Jogos
                    .Where(j => j.Nome.ToLower().Contains(stringPesquisa.ToLower()));

            }
            return View("~/Views/Jogo/JogoList.cshtml", new JogoListViewModel
            {
                Jogos = jogos,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}
