using System.ComponentModel.DataAnnotations;

namespace Vendas.Domain.Entities
{
    public class Filial
    {

        [Key]
        public int FilialId { get; set; }

        [Required]
        [MaxLength(255)]
        public string NomeFilial { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Endereco { get; set; }

        [MaxLength(100)]
        public string? Cidade { get; set; }

        [MaxLength(50)]
        public string? Estado { get; set; }

        public List<Venda> Vendas { get; set; } = new();
    }
}