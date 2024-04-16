using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Models.ViewModels;
using SwitchSelect.Repositorios.Interfaces;
using System.Diagnostics;

namespace SwitchSelect.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        private IJogoRepositorio _jogoRepositorio;

        public HomeController(/**ILogger<HomeController> logger,**/ IJogoRepositorio jogoRepositorio)
        {
            //_logger = logger;
            _jogoRepositorio = jogoRepositorio;
        }

        public IActionResult Error(string? message)
        {
            ViewData["ErrorMessage"] = message;
            return View();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {

            var jogoViewModel = new JogoListViewModel();
            jogoViewModel.Jogos = _jogoRepositorio.Jogos;
            return View(jogoViewModel);
        }




    }
}
