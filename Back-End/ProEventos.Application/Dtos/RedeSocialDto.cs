using ProEventosAPI.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Dtos
{
    public class RedeSocialDto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int EventoID { get; set; }
        public EventoDto Evento { get; set; }
        public int? PalestranteID { get; set; }
        public PalestranteDto Palestrante { get; set; }
    }
}
