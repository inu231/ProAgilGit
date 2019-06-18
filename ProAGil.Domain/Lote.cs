using System;
using ProAgil.Domain;

namespace ProAgil.Domain
{
    public class Lote
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? dataFim { get; set; } 
        public int quantidade { get; set; }
        public int EventoId { get; set; }

        // Lembrar que os relacionamentos devem sempre ser uma propriedade com apenas get; 
        public Evento evento { get; }
    }
}