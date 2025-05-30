using Microsoft.AspNetCore.Mvc;
using ManoelAPI.Models;
using ManoelAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ManoelAPI.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly EmpacotadorService _empacotador;

        public PedidosController(EmpacotadorService empacotador)
        {
            _empacotador = empacotador;
        }

        [Authorize]
        [HttpPost("embalar")]
        public IActionResult EmbalarPedidos([FromBody] PedidoRequest request)
        {
            var resposta = new List<EmpacotamentoResponse>();

            foreach (var pedido in request.Pedidos)
            {
                var caixas = _empacotador.Empacotar(pedido);
                resposta.Add(new EmpacotamentoResponse
                {
                    PedidoId = pedido.Id,
                    Caixas = caixas
                });
            }

            return Ok(resposta);
        }
    }
}
