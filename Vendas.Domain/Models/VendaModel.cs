using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Domain.Models
{
    public class VendaModel
    {
        public int IdVenda { get; set; }
        public string NumeroVenda { get; set; } = string.Empty;
        public DateTime DataVenda { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public decimal ValorTotal { get; set; }
        public string Filial { get; set; } = string.Empty;
        public int IdFilial { get; set; }
        public string StatusVenda { get; set; } = "Não Cancelado"; 
        public List<ItemVendaModel> Itens { get; set; } = new(); 
    }
}
