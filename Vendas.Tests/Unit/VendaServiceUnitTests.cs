using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Vendas.Domain.Entities;
using Vendas.Domain.Interfaces.Repositories;
using Vendas.Domain.Models;
using Vendas.Domain.Services;

namespace Vendas.Tests.Unit
{
    public class VendaServiceTests
    {
        private readonly Mock<IVendaRepository> _vendaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<VendaService>> _loggerMock;
        private readonly VendaService _vendaService;

        public VendaServiceTests()
        {
            _vendaRepositoryMock = new Mock<IVendaRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<VendaService>>();

            _vendaService = new VendaService(_vendaRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AdicionarAsync_Deve_Aplicar_10_Porcento_De_Desconto_Quando_Quantidade_Acima_De_4()
        {
            // Arrange
            var vendaModel = new Faker<VendaModel>()
                .RuleFor(v => v.Itens, f => new List<ItemVendaModel>
                {
                new ItemVendaModel
                {
                    ProdutoId = 1,
                    Quantidade = 5,
                    PrecoUnitario = 100,
                    Desconto = 0
                }
                }).Generate();

            var venda = new Venda();

            _mapperMock.Setup(m => m.Map<Venda>(vendaModel)).Returns(venda);
            _mapperMock.Setup(m => m.Map<VendaModel>(It.IsAny<Venda>())).Returns(vendaModel);
            _vendaRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Venda>())).ReturnsAsync(venda);

            // Act
            var result = await _vendaService.AdicionarAsync(vendaModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            vendaModel.Itens.First().Desconto.Should().Be(10); // 10% de 100
        }

        [Fact]
        public async Task AdicionarAsync_Deve_Aplicar_20_Porcento_De_Desconto_Quando_Quantidade_Entre_10_E_20()
        {
            // Arrange
            var vendaModel = new Faker<VendaModel>()
                .RuleFor(v => v.Itens, f => new List<ItemVendaModel>
                {
                new ItemVendaModel
                {
                    ProdutoId = 1,
                    Quantidade = 15,
                    PrecoUnitario = 100,
                    Desconto = 0
                }
                }).Generate();

            var venda = new Venda();

            _mapperMock.Setup(m => m.Map<Venda>(vendaModel)).Returns(venda);
            _mapperMock.Setup(m => m.Map<VendaModel>(It.IsAny<Venda>())).Returns(vendaModel);
            _vendaRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Venda>())).ReturnsAsync(venda);

            // Act
            var result = await _vendaService.AdicionarAsync(vendaModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            vendaModel.Itens.First().Desconto.Should().Be(20); // 20% de 100
        }

        [Fact]
        public async Task AdicionarAsync_Deve_Retornar_Erro_Quando_Quantidade_Acima_De_20()
        {
            // Arrange
            var vendaModel = new Faker<VendaModel>()
                .RuleFor(v => v.Itens, f => new List<ItemVendaModel>
                {
                new ItemVendaModel
                {
                    ProdutoId = 1,
                    Quantidade = 25,
                    PrecoUnitario = 100,
                    Desconto = 0
                }
                }).Generate();

            // Act
            var result = await _vendaService.AdicionarAsync(vendaModel);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.Mensagem.Should().Contain("Não é possível vender mais de 20 itens iguais");
        }

        [Fact]
        public async Task AdicionarAsync_Nao_Deve_Aplicar_Desconto_Para_Quantidade_Abaixo_De_4()
        {
            // Arrange
            var vendaModel = new Faker<VendaModel>()
                .RuleFor(v => v.Itens, f => new List<ItemVendaModel>
                {
                new ItemVendaModel
                {
                    ProdutoId = 1,
                    Quantidade = 3,
                    PrecoUnitario = 100,
                    Desconto = 0
                }
                }).Generate();

            var venda = new Venda();

            _mapperMock.Setup(m => m.Map<Venda>(vendaModel)).Returns(venda);
            _mapperMock.Setup(m => m.Map<VendaModel>(It.IsAny<Venda>())).Returns(vendaModel);
            _vendaRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Venda>())).ReturnsAsync(venda);

            // Act
            var result = await _vendaService.AdicionarAsync(vendaModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            vendaModel.Itens.First().Desconto.Should().Be(0); // Sem desconto
        }
    }
}
