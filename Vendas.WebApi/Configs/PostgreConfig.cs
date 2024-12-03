using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vendas.Data.Contexts;

namespace Vendas.WebApi.Configs
{
    public static class PostgreConfig
    {
        public static WebApplicationBuilder AddPostgreConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<VendasContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }
    }
}
