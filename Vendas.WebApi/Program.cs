using Microsoft.EntityFrameworkCore;
using Vendas.Data.Contexts;
using Vendas.Data.Repositories;
using Vendas.Data.Seed;
using Vendas.Domain.Interfaces.Repositories;
using Vendas.Domain.Interfaces.Services;
using Vendas.Domain.Services;
using Vendas.WebApi.Configs;
using Vendas.WebApi.Endpoints;
using Vendas.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddFluentValidtionConfig();

builder.AddPostgreConfig();

builder.Services.AddScoped<IVendaService, VendaService>();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();

builder.AddEventDispatcherConfig();
builder.AddLogConfig();
builder.AddAutoMapperConfig();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegistrarEndpoints();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VendasContext>();
    context.Database.Migrate();
    await DataSeeder.SeedClientesAsync(context);
    await DataSeeder.SeedFiliaisAsync(context);
    await DataSeeder.SeedProdutosAsync(context);
}

app.UseMiddleware<ErrorMiddleware>();
app.Run();
