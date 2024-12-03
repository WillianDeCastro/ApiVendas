using AutoMapper;
using Microsoft.Extensions.Logging;
using Vendas.Domain.Entities;
using Vendas.Domain.Interfaces.Repositories;
using Vendas.Domain.Interfaces.Services;
using Vendas.Domain.Models;

namespace Vendas.Domain.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public VendaService(IVendaRepository vendaRepository, ILogger<VendaService> logger, IMapper mapper)
        {
            _vendaRepository = vendaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServicesResponse<List<VendaModel>>> ObterTodasAsync()
        {
            var vendas = await _vendaRepository.ObterTodasAsync();

            if (vendas == null || !vendas.Any())
            {
                return new ServicesResponse<List<VendaModel>>(false, null, "Nenhuma venda encontrada.");
            }

            var modelVendas = _mapper.Map<List<VendaModel>>(vendas);

            return new ServicesResponse<List<VendaModel>>(true, modelVendas, "Vendas obtidas com sucesso.");
        }

        public async Task<ServicesResponse<VendaModel>> ObterPorIdAsync(int idVenda)
        {
            var venda = await _vendaRepository.ObterPorIdAsync(idVenda);

            if (venda == null)
            {
                return new ServicesResponse<VendaModel>(false, null, $"Venda com ID {idVenda} não encontrada.");
            }

            var modelVenda = _mapper.Map<VendaModel>(venda);

            return new ServicesResponse<VendaModel>(true, modelVenda, "Venda obtida com sucesso.");
        }

        public async Task<ServicesResponse<VendaModel>> AdicionarAsync(VendaModel vendaReq)
        {
            vendaReq.DataVenda = DateTime.UtcNow;

            vendaReq.ValorTotal = vendaReq.Itens.Sum(item => item.Quantidade * (item.PrecoUnitario - item.Desconto));

            var venda = _mapper.Map<Venda>(vendaReq);
            var novaVenda = _mapper.Map<VendaModel>(await _vendaRepository.AdicionarAsync(venda));

            return new ServicesResponse<VendaModel>(true, novaVenda, "Venda adicionada com sucesso.");
        }

        public async Task<ServicesResponse<VendaModel>> AtualizarAsync(int vendaId, VendaModel vendaAtualizada)
        {
            var vendaExistente = await _vendaRepository.ObterPorIdAsync(vendaId);

            if (vendaExistente == null)
            {
                return new ServicesResponse<VendaModel>(false, null, $"Venda com ID {vendaId} não encontrada.");
            }

            vendaExistente.ClienteId = vendaAtualizada.IdCliente;
            vendaExistente.FilialId = vendaAtualizada.IdFilial;
            vendaExistente.StatusVenda = vendaAtualizada.StatusVenda;

            vendaExistente.Itens = vendaAtualizada.Itens.ConvertAll(i => new Entities.ItemVenda
            {
                ItemVendaId = i.ItemVendaId,
                VendaId = vendaExistente.VendaId,
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                Desconto = i.Desconto,
                Produto = null!
            });

            vendaExistente.ValorTotal = vendaExistente.Itens.Sum(item => item.Quantidade * (item.PrecoUnitario - item.Desconto));

            var vendaAtualizadaFinal = _mapper.Map<VendaModel>(await _vendaRepository.AtualizarAsync(vendaExistente));

            return new ServicesResponse<VendaModel>(true, vendaAtualizadaFinal, "Venda atualizada com sucesso.");
        }

        public async Task<ServicesResponse<VendaModel>> RemoverAsync(int idVenda)
        {
            var vendaExistente = await _vendaRepository.ObterPorIdAsync(idVenda);
            bool sucesso = true;
            string msg = string.Empty;
            if (vendaExistente == null)
            {
                sucesso = false;
                msg = $"Venda com ID {idVenda} não encontrada.";
            }
            else
            {
                await _vendaRepository.RemoverAsync(idVenda);
                msg = "Venda removida com sucesso.";
            }

            return new ServicesResponse<VendaModel>(sucesso, null, msg);
        }
    }
}
