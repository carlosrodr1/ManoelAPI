using ManoelAPI.Models;

namespace ManoelAPI.Services
{
    public class EmpacotadorService
    {
        public List<CaixaEmpacotada> Empacotar(Pedido pedido)
        {
            var caixasUsadas = new List<CaixaEmpacotada>();
            var produtosRestantes = new List<Produto>(pedido.Produtos);

            foreach (var produto in produtosRestantes)
            {
                var caixa = CaixaDisponivel.Todas
                    .OrderBy(c => c.Volume)
                    .FirstOrDefault(c =>
                        produto.Altura <= c.Altura &&
                        produto.Largura <= c.Largura &&
                        produto.Comprimento <= c.Comprimento);

                if (caixa == null)
                    throw new Exception("Produto não cabe em nenhuma caixa.");

                var caixaExistente = caixasUsadas.FirstOrDefault(c => c.Caixa.Nome == caixa.Nome && c.Produtos.Count < 1);
                if (caixaExistente == null)
                {
                    caixaExistente = new CaixaEmpacotada { Caixa = caixa };
                    caixasUsadas.Add(caixaExistente);
                }

                caixaExistente.Produtos.Add(produto);
            }

            return caixasUsadas;
        }
    }

    public class CaixaEmpacotada
    {
        public Caixa Caixa { get; set; }
        public List<Produto> Produtos { get; set; } = new();
    }
}
