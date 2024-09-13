using Microsoft.AspNetCore.Identity;

namespace ExemploAPI.Models
{
    public class Venda
    {
        public Guid VendaId { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public int? NumeroPedido { get; set; }
        public double? TotalVenda { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public string? UserId { get; set; }

    }
}
