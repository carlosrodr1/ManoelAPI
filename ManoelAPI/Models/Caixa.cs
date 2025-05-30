namespace ManoelAPI.Models
{
    public class Caixa
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }

        public decimal Volume => Altura * Largura * Comprimento;
    }
}
