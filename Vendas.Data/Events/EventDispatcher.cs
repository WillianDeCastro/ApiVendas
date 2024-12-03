using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Domain.Interfaces.Events;

namespace Vendas.Data.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly ILogger _logger;
        private readonly bool _useRabbitMq; // Controle para decidir o comportamento

        public EventDispatcher(ILogger logger, bool useRabbitMq)
        {
            _logger = logger;
            _useRabbitMq = useRabbitMq;
        }

        public async Task DispatchAsync<TEvent>(TEvent evento) where TEvent : class
        {
            if (_useRabbitMq)
            {
                // Exemplo fictício de envio para RabbitMQ
                await SendToRabbitMqAsync(evento);
            }
            else
            {
                // Loga o evento no Serilog
                _logger.LogInformation("Evento disparado: {@Evento}", evento);
            }
        }

        private Task SendToRabbitMqAsync<TEvent>(TEvent evento)
        {
     
            _logger.LogInformation("Enviando evento para RabbitMQ: {@Evento}", evento);
            return Task.CompletedTask;
        }
    }
}
