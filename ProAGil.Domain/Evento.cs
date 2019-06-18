//using System;
using System;
using System.Collections.Generic;
using ProAgil.Domain;

namespace ProAgil.Domain
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Local { get; set; }
        public DateTime DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        public string ImagemURL2 { get; set; }
        public string Lote { get; set; }
        public string telefone { get; set; }
        public string Email { get; set; }
        public List<Lote> Lotes { get; set; }
        public List<RedeSocial> RedesSociais { get; set; }
        public List<PalestranteEvento> PalestranteEventos { get; set; }
    
    }
}