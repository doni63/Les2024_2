﻿using Microsoft.AspNetCore.Mvc;
using SwitchSelect.Service;

namespace SwitchSelect.Controllers;

public class GraficoController : Controller
{
    private readonly GraficoVendasService _graficoVendas;

    public GraficoController(GraficoVendasService graficoVendas)
    {
        _graficoVendas = graficoVendas ?? throw new ArgumentNullException(nameof(graficoVendas));
    }

    public JsonResult VendasJogos(DateTime? dataInicial, DateTime? dataFinal)
    {
        DateTime inicio = dataInicial ?? DateTime.Now.AddMonths(-12);
        DateTime fim = dataFinal ?? DateTime.Now;

        var jogoVendasTotais = _graficoVendas.GetVendas(inicio, fim);
        return Json(jogoVendasTotais);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
