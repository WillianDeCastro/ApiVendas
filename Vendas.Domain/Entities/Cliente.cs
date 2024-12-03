using System.ComponentModel.DataAnnotations;

namespace Vendas.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(255)]
        public string NomeCliente { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; }

        public List<Venda> Vendas { get; set; } = new();
    }
}