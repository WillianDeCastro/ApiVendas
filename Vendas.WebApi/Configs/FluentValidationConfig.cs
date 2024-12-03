using FluentValidation;
using FluentValidation.AspNetCore;
using Vendas.Domain.Validators;

namespace Vendas.WebApi.Configs
{
    public static class FluentValidationConfig
    {
        public static WebApplicationBuilder AddFluentValidtionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<VendaModelValidator>();

            return builder;
        }
    }
}
