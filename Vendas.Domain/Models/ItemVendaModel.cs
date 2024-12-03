namespace Vendas.Domain.Models
{
    public class ItemVendaModel
    {
        public string Produto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotalItem { get; set; }
        public int ItemVendaId { get;  set; }
        public int ProdutoId { get;  set; }
    }
}