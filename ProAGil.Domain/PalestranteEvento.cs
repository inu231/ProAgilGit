using System.Collections.Generic;
using ProAgil.Domain;

namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        public int PalestranteId { get; set; }
        public int EventoId { get; set;}
        public Evento Evento { get; set; }
        public Palestrante Palestrante { get; set; }
        //public List<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}