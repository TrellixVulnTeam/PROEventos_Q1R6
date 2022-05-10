using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class Palestrante
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}
