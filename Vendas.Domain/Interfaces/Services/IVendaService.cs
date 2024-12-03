using Vendas.Domain.Entities;
using Vendas.Domain.Models;

namespace Vendas.Domain.Interfaces.Services
{
    public interface IVendaService
    {
        Task<ServicesResponse<List<VendaModel>>> ObterTodasAsync();
        Task<ServicesResponse<VendaModel>> ObterPorIdAsync(int idVenda);
        Task<ServicesResponse<VendaModel>> AdicionarAsync(VendaModel vendaReq);
        Task<ServicesResponse<VendaModel>> AtualizarAsync(int vendaId, VendaModel vendaAAtualizar);
        Task<ServicesResponse<VendaModel>> RemoverAsync(int idVenda);
    }
}
