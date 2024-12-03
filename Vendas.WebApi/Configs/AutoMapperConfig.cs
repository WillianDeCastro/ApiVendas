using AutoMapper;
using Vendas.Domain.Mappings;

namespace Vendas.WebApi.Configs
{
    public static class AutoMapperConfig
    {
        public static WebApplicationBuilder AddAutoMapperConfig(this WebApplicationBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VendaProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            return builder;
        }
    }
}
