using NUnit.Framework;
using TesteDeveloper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDeveloper.Tests
{
    [TestFixture()] // Indica que a classe é um conjunto de testes
    public class GerenciadorEstoqueTests
    {
        private GerenciadorEstoque gerenciadorEstoque;

        private IList<EstoqueProduto> estoqueProdutos = new List<EstoqueProduto>
        {
            new EstoqueProduto{Referencia = "A2342", SaldoEstoque = 10},
            new EstoqueProduto{Referencia = "B8765", SaldoEstoque = 4},
            new EstoqueProduto{Referencia = "C9546", SaldoEstoque = 6},
            new EstoqueProduto{Referencia = "D7862", SaldoEstoque = 45},
            new EstoqueProduto{Referencia = "E6423", SaldoEstoque = 7}
        };

        [SetUp]
        public void Setup()
        {
            gerenciadorEstoque = new GerenciadorEstoque(estoqueProdutos);
        }

        [Test()]
        [TestCase("A2342", 11, false)]
        [TestCase("A2342", 10, true)]
        [TestCase("A2342", 9, true)]
        [TestCase("C9546", 11, false)]
        [TestCase("D7862", 11, true)]
        [TestCase("E6423", 11, false)]
        public void EstoqueDisponivelTest(string referencia, int quantidadeRequerida, bool expected)
        {
            // Antes: Assert.AreEqual(gerenciadorEstoque.EstoqueDisponivel(referencia, quantidadeRequerida), expected);
            Assert.AreEqual(expected, gerenciadorEstoque.EstoqueDisponivel(referencia, quantidadeRequerida));
        }

        [Test()]
        [TestCase("A2342", 10)]
        [TestCase("C9546", 6)]
        [TestCase("D7862", 45)]
        [TestCase("E6423", 7)]
        public void GetSaldoTest(string referencia, int expected)
        {
            // Antes: Assert.AreEqual(gerenciadorEstoque.GetSaldo(referencia), expected);
            Assert.AreEqual(expected, gerenciadorEstoque.GetSaldo(referencia));
        }

        [Test()] // Teste do método ToString da classe GerenciadorEstoque -- execução independente
        public void ToStringTest()
        {
            string expected = "Referência: A2342 Saldo: 10\nReferência: B8765 Saldo: 4\nReferência: C9546 Saldo: 6\nReferência: D7862 Saldo: 45\nReferência: E6423 Saldo: 7";

            Assert.AreEqual(expected, gerenciadorEstoque.ToString());

        }              
    }                  
}
