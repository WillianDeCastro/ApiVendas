using AutoMapper;
using Vendas.Domain.Entities;
using Vendas.Domain.Models;

namespace Vendas.Domain.Mappings
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<Venda, VendaModel>()
                .ForMember(dest => dest.IdVenda, opt => opt.MapFrom(src => src.VendaId))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente.NomeCliente))
                .ForMember(dest => dest.Filial, opt => opt.MapFrom(src => src.Filial.NomeFilial));

            CreateMap<ItemVenda, ItemVendaModel>()
                .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto.NomeProduto))
                .ForMember(dest => dest.ValorTotalItem, opt => opt.MapFrom(src => src.Quantidade * (src.PrecoUnitario - src.Desconto)));

            CreateMap<VendaModel, Venda>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.FilialId, opt => opt.MapFrom(src => src.IdFilial))
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Filial, opt => opt.Ignore()) 
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));

            CreateMap<ItemVendaModel, ItemVenda>()
                .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProdutoId))
                .ForMember(dest => dest.Produto, opt => opt.Ignore()); 
        }
    }
}
