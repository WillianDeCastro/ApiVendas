using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Data.Events
{
    public class EventoVenda
    {
        public int IdVenda { get; set; }
        public string NumeroVenda { get; set; }
        public DateTime DataCriacao { get; set; }
        public TipoEvento Tipo { get; set; }
    }

    public enum TipoEvento
    {
        CompraCriada,
        CompraAlterada,
        CompraCancelada
    }
}
