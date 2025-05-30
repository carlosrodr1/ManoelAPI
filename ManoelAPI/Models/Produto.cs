namespace ManoelAPI.Models
{
    public class Produto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }

        public decimal Volume => Altura * Largura * Comprimento;
    }
}
