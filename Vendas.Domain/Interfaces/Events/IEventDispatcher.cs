using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Domain.Interfaces.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent evento) where TEvent : class;
    }
}
