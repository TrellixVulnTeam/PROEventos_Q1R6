using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class Lote
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Quantidade { get; set; }
        public int EventoID { get; set; }
        public Evento Evento { get; set; }
    }
}
