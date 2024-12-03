using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendas.Domain.Entities
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroVenda { get; set; } = string.Empty;

        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        [Required]
        public int FilialId { get; set; }

        [ForeignKey("FilialId")]
        public Filial Filial { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string StatusVenda { get; set; } = "Não Cancelado";

        public List<ItemVenda> Itens { get; set; } = new();
    }
}
