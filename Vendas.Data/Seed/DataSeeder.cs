using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Data.Contexts;
using Vendas.Domain.Entities;

namespace Vendas.Data.Seed
{
    public class DataSeeder
    {
        public static async Task SeedClientesAsync(VendasContext context)
        {
            if (!await context.Clientes.AnyAsync())
            {
                var clientes = new List<Cliente>
            {
                new Cliente
                {
                    NomeCliente = "Cliente Teste 1",
                    Email = "cliente1@example.com",
                    Telefone = "123456789"
                },
                new Cliente
                {
                    NomeCliente = "Cliente Teste 2",
                    Email = "cliente2@example.com",
                    Telefone = "987654321"
                }
            };

                await context.Clientes.AddRangeAsync(clientes);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedFiliaisAsync(VendasContext context)
        {
            if (!await context.Filiais.AnyAsync())
            {
                var filial = new Filial
                {
                    NomeFilial = "Teste Filial1",
                    Estado = "Estadual",
                    Cidade = "Cidadela",
                    Endereco = "Somewhere"
                };

                await context.Filiais.AddAsync(filial);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedProdutosAsync(VendasContext context)
        {
            if (!await context.Produtos.AnyAsync())
            {
                var produto = new Produto
                {
                    NomeProduto = "Tst Produto1",
                    PrecoUnitario = 14.5m,
                };

                await context.Produtos.AddAsync(produto);
                await context.SaveChangesAsync();
            }
        }
    }
}
