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

        [Test()] // Confirma que quantidadeRequerida igual a 0 ou negativa é sempre considerada disponível
        [TestCase("A2342", 0, true)]
        [TestCase("A2342", -1, true)]
        public void EstoqueDisponivelCasosLimiteTest(string referencia, int quantidadeRequerida, bool expected)
        {
            Assert.AreEqual(expected, gerenciadorEstoque.EstoqueDisponivel(referencia, quantidadeRequerida));
        }

        [Test()] // Confirma que a busca por referência ignora maiúsculas/minúsculas
        public void GetSaldoCaseInsensitiveTest()
        {
            Assert.AreEqual(10, gerenciadorEstoque.GetSaldo("a2342"));
        }

        [Test()] // Confirma que retorna string vazia com lista sem itens
        public void ToStringComListaVaziaTest()
        {
            var gerenciadorVazio = new GerenciadorEstoque(new List<EstoqueProduto>());

            Assert.AreEqual(string.Empty, gerenciadorVazio.ToString());
        }

        [Test()] // Confirma que referência não cadastrada lança KeyNotFoundException com a mensagem esperada
        public void GetSaldoReferenciaInexistenteTest()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => gerenciadorEstoque.GetSaldo("XPTO"));
            Assert.AreEqual("Saldo indisponível para o item XPTO", ex.Message);
        }

        [Test()] // Confirma que EstoqueDisponivel propaga o mesmo erro de GetSaldo para referência não cadastrada
        public void EstoqueDisponivelReferenciaInexistenteTest()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => gerenciadorEstoque.EstoqueDisponivel("XPTO", 1));
            Assert.AreEqual("Saldo indisponível para o item XPTO", ex.Message);
        }

        [Test()] // Confirma que o construtor rejeita lista nula
        public void ConstrutorComListaNulaTest()
        {
            Assert.Throws<ArgumentNullException>(() => new GerenciadorEstoque(null));
        }

        [Test()] // Confirma que GetSaldo rejeita referência nula ou vazia com ArgumentException, distinguindo de referência não cadastrada
        [TestCase(null)]
        [TestCase("")]
        public void GetSaldoReferenciaNulaOuVaziaTest(string referencia)
        {
            Assert.Throws<ArgumentException>(() => gerenciadorEstoque.GetSaldo(referencia));
        }

        [Test()] // Confirma que EstoqueDisponivel propaga a mesma validação de GetSaldo para referência nula ou vazia
        [TestCase(null)]
        [TestCase("")]
        public void EstoqueDisponivelReferenciaNulaOuVaziaTest(string referencia)
        {
            Assert.Throws<ArgumentException>(() => gerenciadorEstoque.EstoqueDisponivel(referencia, 1));
        }

        [Test()] // Teste do método ToString da classe GerenciadorEstoque -- execução independente
        public void ToStringTest()
        {
            string expected = "Referência: A2342 Saldo: 10\nReferência: B8765 Saldo: 4\nReferência: C9546 Saldo: 6\nReferência: D7862 Saldo: 45\nReferência: E6423 Saldo: 7";

            Assert.AreEqual(expected, gerenciadorEstoque.ToString());

        }
    }
}
