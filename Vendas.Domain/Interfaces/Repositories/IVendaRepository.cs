using Vendas.Domain.Entities;

namespace Vendas.Domain.Interfaces.Repositories
{
    public interface IVendaRepository
    {
        Task<List<Venda>> ObterTodasAsync();
        Task<Venda?> ObterPorIdAsync(int id);
        Task<Venda> AdicionarAsync(Venda venda);
        Task<Venda> AtualizarAsync(Venda venda);
        Task RemoverAsync(int id);
    }
}
