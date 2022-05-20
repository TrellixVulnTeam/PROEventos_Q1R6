using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class EventoPersistence : IEventoPersistence
    {
        private readonly ProEventosContext _context;
        public EventoPersistence(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            IQueryable<Evento> Query = _context.Eventos.Include(e => e.Lote).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                Query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            Query = Query.AsNoTracking().Where(e => (e.Tema.ToLower().Contains(pageParams.Termos.ToLower()) ||
                                                     e.Local.ToLower().Contains(pageParams.Termos.ToLower())) && e.UserID == userId).OrderBy(e=> e.ID);
            return await PageList<Evento>.CreateAsync(Query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Evento> GetAllEventoByIdAsync(int userId, int EventoID, bool includePalestrantes)
        {
            IQueryable<Evento> Query = _context.Eventos.Include(e => e.Lote).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                Query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            Query = Query.AsNoTracking().OrderBy(e => e.ID).Where(e => e.ID == EventoID && e.UserID == userId);
            return await Query.FirstOrDefaultAsync();
        }
    }
}
