using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public ProAgilContext _context { get; }
        public ProAgilRepository(ProAgilContext _context)
        {
            this._context = _context;
        }

        // Métodos gerais: Estes métodos podem receber todas as nossas classes desta forma: ss
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);

            // Marca para todas as queries. Para não deixar o ambiente rastreável,ou seja, não deixa o ambiente travado quando é feito uma query.
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        // Se salvou alguem, retorna maior q 0
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetALLEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes) {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetALLEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes) {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query
                .AsNoTracking()
                .OrderByDescending(c => c.DataEvento)
                .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes) {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query
                .AsNoTracking()
                .OrderByDescending(c => c.DataEvento)
                .Where(c => c.EventoId.Equals(EventoId));

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);
              
            if (includeEventos) {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(E => E.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .Where(i => i.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetALLPalestrantesAsyncByName(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);
              
            if (includeEventos) {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(E => E.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .Where(i => i.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetALLPalestrantesAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);
              
            if (includeEventos) {
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(E => E.Evento);
            }

            query = query.AsNoTracking().OrderBy(c => c.Nome);
            return await query.ToArrayAsync();
        }


    }
}