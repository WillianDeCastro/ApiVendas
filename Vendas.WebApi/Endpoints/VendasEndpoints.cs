using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vendas.Data.Events;
using Vendas.Domain.Interfaces.Events;
using Vendas.Domain.Interfaces.Services;
using Vendas.Domain.Models;

namespace Vendas.WebApi.Endpoints
{
    public static class VendasEndpoints
    {
        public static void MapVendasEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Vendas", HandleGetAll)
               .WithName("TodasAsVendas")
               .WithOpenApi();

            app.MapGet("/api/Vendas/{idVenda}", HandleGetById)
               .WithName("Venda")
               .WithOpenApi();

            app.MapPut("/api/Vendas/{idVenda}", HandleUpdate)
               .WithName("Atualizar")
               .WithOpenApi();

            app.MapPost("/api/Vendas", HandleCreate)
               .WithName("RegistrarVenda")
               .WithOpenApi();

            app.MapDelete("/api/Vendas/{idVenda}", HandleDelete)
               .WithName("RemoverVenda")
               .WithOpenApi();
        }


        private static async Task<IResult> HandleGetAll([FromServices] IVendaService service, ILogger<Program> logger)
        {
            logger.LogInformation("Executando o método GET no endpoint /vendas");
            var entidade = await service.ObterTodasAsync();
            return Results.Ok(entidade);
        }

        private static async Task<IResult> HandleGetById([FromServices] IVendaService service, ILogger<Program> logger, int idVenda)
        {
            logger.LogInformation("Executando o método GET no endpoint /vendas/idVenda");
            var entidade = await service.ObterPorIdAsync(idVenda);
            return entidade != null ? Results.Ok(entidade) : Results.NotFound($"Venda com ID {idVenda} não encontrada.");
        }

        private static async Task<IResult> HandleUpdate([FromServices] IVendaService service, IEventDispatcher eventDispatcher, ILogger<Program> logger, int idVenda, [FromBody] VendaModel venda)
        {
            logger.LogInformation("Executando o método PUT no endpoint /vendas/idVenda");
            var entidade = await service.AtualizarAsync(idVenda, venda);

            await eventDispatcher.DispatchAsync(new EventoVenda
            {
                IdVenda = entidade.Objeto.IdVenda,
                NumeroVenda = entidade.Objeto.NumeroVenda,
                DataCriacao = DateTime.UtcNow,
                Tipo = TipoEvento.CompraAlterada
            });

            return Results.Ok(entidade);
        }

        private static async Task<IResult> HandleCreate(
            [FromServices] IVendaService service,
            [FromServices] IValidator<VendaModel> validator,
             IEventDispatcher eventDispatcher,
            ILogger<Program> logger,
            [FromBody] VendaModel vendaReq)
        {
            logger.LogInformation("Executando o método POST no endpoint /vendas/idVenda");

            var validationResult = validator.Validate(vendaReq);

            if (!validationResult.IsValid)
            {
                logger.LogWarning($"Objeto inválido, erros: {string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage))}");
                return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var entidade = await service.AdicionarAsync(vendaReq);

            await eventDispatcher.DispatchAsync(new EventoVenda
            {
                IdVenda = entidade.Objeto.IdVenda,
                NumeroVenda = entidade.Objeto.NumeroVenda,
                DataCriacao = DateTime.UtcNow,
                Tipo = TipoEvento.CompraCriada
            });

            return Results.Created($"/api/Vendas/{entidade.Objeto.NumeroVenda}", entidade);
        }

        private static async Task<IResult> HandleDelete(int idVenda, [FromServices] IVendaService service, IEventDispatcher eventDispatcher, ILogger<Program> logger)
        {
            logger.LogInformation("Executando o método DELETE no endpoint /vendas/idVenda");
            var entidade = await service.RemoverAsync(idVenda);

            await eventDispatcher.DispatchAsync(new EventoVenda
            {
                IdVenda = entidade.Objeto.IdVenda,
                NumeroVenda = entidade.Objeto.NumeroVenda,
                DataCriacao = DateTime.UtcNow,
                Tipo = TipoEvento.CompraCancelada
            });

            return Results.Ok(entidade);
        }
    }
}
