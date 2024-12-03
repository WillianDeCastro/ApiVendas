using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vendas.Domain.Entities
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [MaxLength(255)]
        public string NomeProduto { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }

        public List<ItemVenda> ItensVenda { get; set; } = new();
    }
}