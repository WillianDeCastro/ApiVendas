using Microsoft.EntityFrameworkCore;
using Vendas.Data.Contexts;
using Vendas.Domain.Entities;
using Vendas.Domain.Interfaces.Repositories;

namespace Vendas.Data.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly VendasContext _context;

        public VendaRepository(VendasContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> ObterTodasAsync()
        {
            return await _context.Vendas.AsNoTracking()
                .Include(v => v.Cliente)
                .Include(v => v.Filial)
                .Include(v => v.Itens)
                    .ThenInclude(i => i.Produto)
                .ToListAsync();
        }

        public async Task<Venda?> ObterPorIdAsync(int id)
        {
            return await _context.Vendas.AsNoTracking()
                .Include(v => v.Cliente)
                .Include(v => v.Filial)
                .Include(v => v.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(v => v.VendaId == id);
        }

        public async Task<Venda> AdicionarAsync(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
            return venda;
        }

        public async Task<Venda> AtualizarAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
            return venda;
        }

        public async Task RemoverAsync(int id)
        {
            var venda = await ObterPorIdAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
            }
        }
    }
}
