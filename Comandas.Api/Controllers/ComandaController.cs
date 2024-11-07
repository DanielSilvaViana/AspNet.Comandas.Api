﻿using Comandas.Api.Data;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComandaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Comanda>> GetComandas()
        {
            return _context.Comandas.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Comanda> GetComanda(string id)
        {
            var comanda = _context.Comandas.Find(id);
            if (comanda == null) return NotFound();
            return comanda;
        }

        [HttpPost]
        public ActionResult<Comanda> PostComanda(Comanda comanda)
        {
            _context.Comandas.Add(comanda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetComanda), new { id = comanda.Id }, comanda);
        }

        [HttpPut("{id}")]
        public IActionResult PutComanda(int id, Comanda comanda)
        {
            if (id != comanda.Id) return BadRequest();

            _context.Entry(comanda).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Comandas.Any(c => c.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComanda(string id)
        {
            var comanda = _context.Comandas.Find(id);
            if (comanda == null) return NotFound();

            _context.Comandas.Remove(comanda);
            _context.SaveChanges();

            return NoContent();
        }
    }

}