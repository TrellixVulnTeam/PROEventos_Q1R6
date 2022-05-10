using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class PalestranteEvento
    {
        public int ID { get; set; }
        public Palestrante Palestrante { get; set; }
        public int EventoID { get; set; }
        public Evento Evento { get; set; }
    }
}
