using FluentValidation.AspNetCore;
using FluentValidation;
using Vendas.Domain.Validators;
using Vendas.Data.Events;
using Vendas.Domain.Interfaces.Events;

namespace Vendas.WebApi.Configs
{
    public static class EventDispatcherConfig
    {
        public static WebApplicationBuilder AddEventDispatcherConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IEventDispatcher>(sp =>
            {
                var logger = sp.GetRequiredService<Microsoft.Extensions.Logging.ILogger<IEventDispatcher>>();
                bool useRabbitMq = builder.Configuration.GetValue<bool>("UseRabbitMq");
                return new EventDispatcher(logger, useRabbitMq);
            });

            return builder;
        }
    }
}
