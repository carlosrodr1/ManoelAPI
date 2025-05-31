using System;
using System.Collections.Generic;
using System.Linq;
using ManoelAPI.Models;
using ManoelAPI.Services;
using Xunit;

namespace ManoelAPI.Tests.Services
{
    public class EmpacotadorServiceTests
    {
        private Produto CriarProduto(string id, decimal a, decimal l, decimal c) =>
            new Produto
            {
                ProdutoId = id,
                Dimensoes = new Dimensoes { Altura = a, Largura = l, Comprimento = c }
            };

        [Fact]
        public void Empacota_UmPorCaixa()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    CriarProduto("P1", 10, 10, 10),
                    CriarProduto("P2", 25, 35, 40)
                }
            };

            var caixas = service.Empacotar(pedido);
            Assert.True(caixas.Count >= 1 && caixas.Count <= 2);
        }

        [Fact]
        public void ProdutoGrande_RetornaComObservacao()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    CriarProduto("Cadeira Gamer", 1000, 1000, 1000)
                }
            };

            var caixas = service.Empacotar(pedido);
            Assert.Single(caixas);
            Assert.Null(caixas[0].Caixa);
            Assert.Single(caixas[0].Produtos);
            Assert.Equal("Cadeira Gamer", caixas[0].Produtos[0].ProdutoId);
            Assert.NotNull(caixas[0].Observacao);
            Assert.Contains("não cabe", caixas[0].Observacao, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void UsaCaixasSeparadas_QuandoProdutosGrandes()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    CriarProduto("G1", 50, 50, 60),
                    CriarProduto("G2", 50, 50, 60)
                }
            };

            var caixas = service.Empacotar(pedido);
            Assert.Equal(2, caixas.Count);
        }

        [Fact]
        public void ReaproveitaCaixa_QuandoHaEspaco()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    CriarProduto("X1", 10, 10, 10),
                    CriarProduto("X2", 10, 10, 10),
                    CriarProduto("X3", 10, 10, 10)
                }
            };

            var caixas = service.Empacotar(pedido);
            Assert.True(caixas.Count < 3);
        }

        [Fact]
        public void UsaUmaCaixa_QuandoProdutosPequenos()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    CriarProduto("S1", 20, 20, 20),
                    CriarProduto("S2", 10, 10, 10)
                }
            };

            var caixas = service.Empacotar(pedido);
            Assert.Single(caixas);
        }

        [Fact]
        public void RetornaVazio_QuandoSemProdutos()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido { Produtos = new List<Produto>() };

            var caixas = service.Empacotar(pedido);
            Assert.Empty(caixas);
        }

        [Fact]
        public void EncaixaExato_QuandoProdutoTemMedidaLimite()
        {
            var service = new EmpacotadorService();
            var pedido = new Pedido
            {
                Produtos = new List<Produto>
                {
                    CriarProduto("Limite", 30, 40, 80)
                }
            };

            var caixas = service.Empacotar(pedido);
            Assert.Single(caixas);
            Assert.Single(caixas[0].Produtos);
        }
    }
}
