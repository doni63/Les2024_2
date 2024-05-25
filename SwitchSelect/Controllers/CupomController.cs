﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SwitchSelect.Data;
using SwitchSelect.Models;
using SwitchSelect.Service;

namespace SwitchSelect.Controllers
{
    public class CupomController : Controller
    {
        private readonly SwitchSelectContext _context;
        private readonly Cupom _cupom;

        public CupomController(SwitchSelectContext context)
        {
            _context = context;
        }

        // GET: Cupom
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cupons.ToListAsync());
        }

        // GET: Cupom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupom = await _context.Cupons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupom == null)
            {
                return NotFound();
            }

            return View(cupom);
        }

        // GET: Cupom/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Cupom cupom)
        {

            if (ModelState.IsValid)
            {
                var codigoCupom = new Cupom();
                var codigo = new StringBuilder();
                codigo.Append("promo-");
                codigo.Append("R$" + cupom.Valor);
                codigo.Append("-");
                codigo.Append(codigoCupom.GerarCodigoCupom());
                cupom.CodigoCupom = codigo.ToString();

                cupom.Status = "Valido";

                _context.Add(cupom);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Cupom");
            }
            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null)
            {
                return NotFound();
            }
            return View(cupom);
        }


        // GET: Cupom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupom = await _context.Cupons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupom == null)
            {
                return NotFound();
            }

            return View(cupom);
        }

        // POST: Cupom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom != null)
            {
                _context.Cupons.Remove(cupom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool CupomExists(int id)
        //{
        //    return _context.Cupons.Any(e => e.Id == id);
        //}
        public async Task<IActionResult> CupomListCliente(int clienteId)
        {
            var cupomCliente = await _context.Cupons
                .Where(c => c.ClienteId == clienteId)
                .ToListAsync();

            return View(cupomCliente);
        }

        

    }
}
