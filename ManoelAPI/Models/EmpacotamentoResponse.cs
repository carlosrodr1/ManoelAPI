using System.Text.Json.Serialization;

namespace ManoelAPI.Models
{
    public class EmpacotamentoResponse
    {
        [JsonPropertyName("pedido_id")]
        public Guid PedidoId { get; set; }

        [JsonPropertyName("caixas")]
        public List<CaixaEmpacotadaResponse> Caixas { get; set; } = new();

    }
}
