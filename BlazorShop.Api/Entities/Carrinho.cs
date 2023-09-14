namespace BlazorShop.Api.Entities
{
    public class Carrinho : EntityBase
    {
        public string UsuarioId { get; set; }
        public ICollection<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
    }
}
