using Serilog;
using Vendas.Data.Events;
using Vendas.Domain.Interfaces.Events;

namespace Vendas.WebApi.Configs
{
    public static class LogConfig
    {
        public static WebApplicationBuilder AddLogConfig(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .CreateLogger();
            builder.Host.UseSerilog();
            return builder;
        }
    }
}
