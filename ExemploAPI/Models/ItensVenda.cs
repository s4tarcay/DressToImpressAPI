namespace ExemploAPI.Models
{
    public class ItensVenda
    {
        public Guid ItensVendaId { get; set; }
        public Guid VendaId { get; set; }
        public Venda? Venda { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int QtdadeItem { get; set; }
        public double? TotalItens { get; set; }
    }
}
