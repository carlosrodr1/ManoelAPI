using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManoelAPI.Models;
using ManoelAPI.Services;
using Xunit;

namespace ManoelAPI.Tests.Services
{
    public class EmpacotadorServiceTests
    {
        [Fact]
        public void Empacota_UmPorCaixa()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    new Produto { Altura = 10, Largura = 20, Comprimento = 30 },
                    new Produto { Altura = 25, Largura = 35, Comprimento = 40 }
                }
            };

            var caixas = service.Empacotar(pedido);

            Assert.Equal(2, caixas.Count);
            Assert.All(caixas, c => Assert.Single(c.Produtos));
        }

        [Fact]
        public void ProdutoInvalido_LancaErro()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
        {
            new Produto { Altura = 1000, Largura = 1000, Comprimento = 1000 }
        }
            };

            var ex = Assert.Throws<Exception>(() => service.Empacotar(pedido));
            Assert.Equal("Produto não cabe em nenhuma caixa.", ex.Message);
        }

    }
}
