using ManoelAPI.Models;

namespace ManoelAPI.Services
{
    public class EmpacotadorService
    {
        public List<CaixaEmpacotada> Empacotar(Pedido pedido)
        {
            var caixasUsadas = new List<CaixaEmpacotada>();
            var produtosNaoEmpacotados = new List<Produto>();

            foreach (var produto in pedido.Produtos)
            {
                var caixa = CaixaDisponivel.Todas
                    .OrderBy(c => c.Volume)
                    .FirstOrDefault(c =>
                        produto.Altura <= c.Altura &&
                        produto.Largura <= c.Largura &&
                        produto.Comprimento <= c.Comprimento);

                if (caixa == null)
                {
                    produtosNaoEmpacotados.Add(produto);
                    continue;
                }

                var caixaExistente = caixasUsadas
                    .Where(c => c.Caixa.Nome == caixa.Nome)
                    .FirstOrDefault(c => c.VolumeOcupado() + produto.Volume <= c.Caixa.Volume);

                if (caixaExistente == null)
                {
                    caixaExistente = new CaixaEmpacotada { Caixa = caixa };
                    caixasUsadas.Add(caixaExistente);
                }

                caixaExistente.Produtos.Add(produto);
            }

            if (produtosNaoEmpacotados.Any())
            {
                caixasUsadas.Add(new CaixaEmpacotada
                {
                    Caixa = null,
                    Produtos = produtosNaoEmpacotados,
                    Observacao = "Produto não cabe em nenhuma caixa disponível."
                });
            }

            return caixasUsadas;
        }

    }

    public class CaixaEmpacotada
    {
        public Caixa Caixa { get; set; }
        public List<Produto> Produtos { get; set; } = new();
        public string Observacao { get; set; }

        public decimal VolumeOcupado() => Produtos.Sum(p => p.Volume);


    }
}
