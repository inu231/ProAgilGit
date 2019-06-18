using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        // Geral
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        // Eventos
        Task<Evento[]> GetALLEventoAsyncByTema(string tema, bool includePalestrantes);
        Task<Evento[]> GetALLEventoAsync(bool includePalestrantes);
        Task<Evento> GetAEventoAsyncById(int EventoId, bool includePalestrantes);

        // Palestrante
        Task<Palestrante[]> GetALLPalestrantesAsync(bool includeEventos);
        Task<Palestrante[]> GetALLPalestrantesAsyncByName(string nome, bool includeEventos);
        Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos);
    }
}