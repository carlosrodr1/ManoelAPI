using ManoelAPI.Services;

namespace ManoelAPI.Models
{
    public class EmpacotamentoResponse
    {
        public Guid PedidoId { get; set; }
        public List<CaixaEmpacotada> Caixas { get; set; } = new();
    }
}
