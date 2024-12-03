using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Vendas.Data.Contexts;
using Vendas.Data.Events;
using Vendas.Data.Repositories;
using Vendas.Data.Seed;
using Vendas.Domain.Interfaces.Events;
using Vendas.Domain.Interfaces.Repositories;
using Vendas.Domain.Interfaces.Services;
using Vendas.Domain.Mappings;
using Vendas.Domain.Services;
using Vendas.Domain.Validators;
using Vendas.WebApi.Endpoints;
using Vendas.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<VendaModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VendaValidator>();

builder.Services.AddDbContext<VendasContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVendaService, VendaService>();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();


builder.Services.AddSingleton<IEventDispatcher>(sp =>
{
    var logger = sp.GetRequiredService<Microsoft.Extensions.Logging.ILogger<IEventDispatcher>>();
    bool useRabbitMq = builder.Configuration.GetValue<bool>("UseRabbitMq");
    return new EventDispatcher(logger, useRabbitMq);
});


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) 
    .Enrich.FromLogContext()
    .WriteTo.Console() 
    .CreateLogger();
builder.Host.UseSerilog();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new VendaProfile()); 
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


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
