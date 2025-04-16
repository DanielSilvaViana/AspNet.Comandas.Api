using Comandas.Api.Data;
using Comandas.Data.Interfaces;
using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using Comandas.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repository
{
    public class MesaRepository : IMesaRepository
    {
        public readonly AppDbContext _context;
        public readonly IComandaRepository _comandaRepository;


        public MesaRepository(AppDbContext appDbContext, IComandaRepository comandaRepository)
        {
            _context = appDbContext;
            _comandaRepository = comandaRepository;
        }

        public async Task<Mesa> DeleteMesaAsync(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                throw new NotFoundException("Mesa não localizada!");
            }

            _context.Mesas.Remove(mesa);
            await _comandaRepository.SaveChangesAsync();
            return mesa;
        }

        public async Task<IEnumerable<MesaDto>> GetMesa()
        {
            var mesa = await _context.Mesas.Select(m => new MesaDto
            {
                Id = m.Id,
                NumeroMesa = m.NumeroMesa,
                SituacaoMesa = m.SituacaoMesa,
            }).ToListAsync();

            return mesa;
        }

        public async Task<Mesa> GetMesaAsync(int numeroMesa)
        {
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesa);
            return mesa;
        }

        public async Task<MesaDto> GetMesaByIdAsync(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);

            if (mesa == null)
            {
                throw new NotFoundException("Mesa Não encontrada!");
            }

            var retornoMesa = new MesaDto
            {
                Id = mesa.Id,
                NumeroMesa = mesa.NumeroMesa,
                SituacaoMesa = mesa.SituacaoMesa
            };
            return retornoMesa;
        }

        public Task<ComandaDto> PostComandaAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Mesa> PostMesaAsync(MesaCreateDto mesaDto)
        {
            var mesa = new Mesa
            {
                NumeroMesa = mesaDto.NumeroMesa,
                SituacaoMesa = mesaDto.SituacaoMesa
            };

            _context.Mesas.Add(mesa);
            await _comandaRepository.SaveChangesAsync();
            return mesa;
        }

        public async Task PutMesaAsync(MesaUpdateDto mesadto, int id)
        {
            if (id != mesadto.Id)
            {
                throw new BadRequestException("Id não localizado!");
            }

            //Consultar e Obter mesa via banco

            var mesa = await _context.Mesas.FindAsync(id);

            if (mesa == null)
            {
                throw new NotFoundException("Mesa Não Localizada!");

            }

            // Atribuir as propriedades das mesas no banco

            mesa.NumeroMesa = mesadto.NumeroMesa;
            mesa.SituacaoMesa = mesadto.SituacaoMesa;

            try
            {
                await _comandaRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesaExists(id))
                {
                    throw new NotFoundException("Mesa não localizada!");
                }
                else
                {
                    throw;
                }
            }
          
        }      

        private bool MesaExists(int id)
        {
            return _context.Mesas.Any(e => e.Id == id);
        }
      
    }
}

