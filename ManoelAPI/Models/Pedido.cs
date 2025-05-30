namespace ManoelAPI.Models
{
    public class Pedido
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Produto> Produtos { get; set; } = new();
    }
}
