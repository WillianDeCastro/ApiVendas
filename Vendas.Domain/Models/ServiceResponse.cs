using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Domain.Models
{
    public class ServicesResponse<T> where T : class
    {
        public ServicesResponse(bool sucesso, T objeto, string mensagem = null)
        {
            Sucesso = sucesso;
            Objeto = objeto;
            Mensagem = mensagem;
        }
        public bool Sucesso { get; set; }
        public T Objeto { get; set; }
        public string Mensagem { get; set; }
    }
}
