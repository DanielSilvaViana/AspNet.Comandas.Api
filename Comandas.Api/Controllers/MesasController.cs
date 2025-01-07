using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Api.Data;
using Comandas.Api.Models;
using Comandas.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Comandas.Services.Interfaces;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Tags("2. Mesas")]

    public class MesasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMesasServices _mesaServices;

        public MesasController(AppDbContext context, IMesasServices mesasServices)
        {
            _context = context;
            _mesaServices = mesasServices;
        }

        // GET: api/Mesas
        /// <summary>
        /// Retorna lista Mesa
        /// </summary>
        /// <returns>Retorna uma Lista de IEnumerable<MesaDto></returns>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<MesaDto>>> GetMesa()
        {
            var mesa =  await _context.Mesas.Select(m => new MesaDto
            {
                Id = m.Id,
                NumeroMesa = m.NumeroMesa,
                SituacaoMesa = m.SituacaoMesa,                
            }).ToListAsync();

            return Ok(mesa);
        }

        // GET: api/Mesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MesaDto>> GetMesa(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);

            if (mesa == null)
            {
                return NotFound("Mesa Não encontrada!");
            }

            var retornoMesa =  new MesaDto
            {
                Id = mesa.Id,
                NumeroMesa = mesa.NumeroMesa,
                SituacaoMesa = mesa.SituacaoMesa                
            };
            return Ok(retornoMesa);
        }

        // PUT: api/Mesas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesa(int id, MesaUpdateDto mesadto)
        {
            if (id != mesadto.Id)
            {
                return BadRequest();
            }

            //Consultar e Obter mesa via banco

            var mesa = await _context.Mesas.FindAsync(id);

            if(mesa == null)
            {
                return NotFound();
            }

            // Atribuir as propriedades das mesas no banco

            mesa.NumeroMesa = mesadto.NumeroMesa;
            mesa.SituacaoMesa = mesadto.SituacaoMesa;            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Mesas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MesaCreateDto>> PostMesa(MesaCreateDto mesaDto)
        {
            var mesa = new Mesa
            {
                NumeroMesa = mesaDto.NumeroMesa,
                SituacaoMesa = mesaDto.SituacaoMesa
            };

            _context.Mesas.Add(mesa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMesa", new { id = mesa.Id }, mesa);
        }

        // DELETE: api/Mesas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMesa(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }

            _context.Mesas.Remove(mesa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MesaExists(int id)
        {
            return _context.Mesas.Any(e => e.Id == id);
        }
    }
}
