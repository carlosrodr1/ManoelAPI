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
            if (request?.Pedidos == null || !request.Pedidos.Any())
                return BadRequest("A requisição deve conter pelo menos um pedido.");

            foreach (var pedido in request.Pedidos)
            {
                try
                {
                    var caixasInternas = _empacotador.Empacotar(pedido);

                    var caixasFormatadas = caixasInternas.Select(c => new CaixaEmpacotadaResponse
                    {
                        CaixaId = c.Caixa?.Nome,
                        Produtos = c.Produtos.Select(p => p.ProdutoId).ToList(),
                        Observacao = c.Observacao
                    }).ToList();


                    resposta.Add(new EmpacotamentoResponse
                    {
                        PedidoId = pedido.Id,
                        Caixas = caixasFormatadas
                    });
                }
                catch
                {
                    continue;
                }
            }


            return Ok(resposta);
        }
    }
}
