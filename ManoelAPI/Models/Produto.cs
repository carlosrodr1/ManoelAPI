using System.Text.Json.Serialization;

namespace ManoelAPI.Models
{
    public class Produto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; }

        [JsonPropertyName("dimensoes")]
        public Dimensoes Dimensoes { get; set; }

        public decimal Altura => Dimensoes?.Altura ?? 0;
        public decimal Largura => Dimensoes?.Largura ?? 0;
        public decimal Comprimento => Dimensoes?.Comprimento ?? 0;
        public decimal Volume => Altura * Largura * Comprimento;
    }

    public class Dimensoes
    {
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
    }
}
