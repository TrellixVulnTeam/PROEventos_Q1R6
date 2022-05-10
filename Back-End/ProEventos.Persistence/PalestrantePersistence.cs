using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;
        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
        }
        public async Task<Palestrante[]> GetAllPalestranteAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> Query = _context.Palestrantes.Include(p => p.RedesSociais);
            if (includeEventos)
            {
                Query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            Query = Query.AsNoTracking().OrderBy(p => p.ID);
            return await Query.ToArrayAsync();
        }
        public async Task<Palestrante[]> GetAllPalestranteByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> Query = _context.Palestrantes.Include(p => p.RedesSociais);
            if (includeEventos)
            {
                Query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            Query = Query.AsNoTracking().OrderBy(p => p.ID).Where(p => p.User.PrimeiroNome.ToLower().Contains(nome.ToLower()) &&
                                                                   p.User.UltimoNome.ToLower().Contains(nome.ToLower()));
            return await Query.ToArrayAsync();
        }
        public async Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteID, bool includeEventos)
        {
            IQueryable<Palestrante> Query = _context.Palestrantes.Include(p => p.RedesSociais);
            if (includeEventos)
            {
                Query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            Query = Query.AsNoTracking().OrderBy(p => p.ID == palestranteID);
            return await Query.FirstOrDefaultAsync();
        }
    }
}
