# ApiVendas
Api utilizando .Net8 e modelo minimal api´s
Infos:
RepositoryPattern
Log com Serilog
Disparo de Eventos
Middleware para centralização de exceptions
Fluent Validations
Entity Framework com Migrations
Seed de dados básicos
Banco PostgreSQL
AutoMapper

Comandos
dotnet ef migrations add NOMEMIGRATION --project ..\Vendas.Data\Vendas.Data.csproj --startup-project .\Vendas.WebApi.csproj
 
Exemplo payload
{
  "idVenda": 1,
  "numeroVenda": "123456",
  "dataVenda": "2024-12-03T06:26:04.081Z",
  "cliente": "Cliente Fictício",
  "idCliente": 1,
  "valorTotal": 100.50,
  "filial": "Filial Central",
  "idFilial": 1,
  "statusVenda": "Não Cancelado",
  "itens": [
    {
      "produto": "Produto Fictício 1",
      "quantidade": 2,
      "precoUnitario": 50.00,
      "desconto": 0,
      "valorTotalItem": 100.00,
      "itemVendaId": 1,
      "produtoId": 1
    }
  ]
}
