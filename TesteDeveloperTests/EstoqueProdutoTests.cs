using NUnit.Framework;
using TesteDeveloper;
using System;

namespace TesteDeveloper.Tests
{
    [TestFixture()]
    public class EstoqueProdutoTests
    {
        [Test()] // Confirma que SaldoEstoque negativo é rejeitado com ArgumentOutOfRangeException
        public void SaldoEstoqueNegativoTest()
        {
            var produto = new EstoqueProduto { Referencia = "A2342" };

            Assert.Throws<ArgumentOutOfRangeException>(() => produto.SaldoEstoque = -1);
        }

        [Test()] // Confirma que SaldoEstoque zero é aceito (produto sem estoque, mas cadastrado)
        public void SaldoEstoqueZeroTest()
        {
            var produto = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 0 };

            Assert.AreEqual(0, produto.SaldoEstoque);
        }

        [Test()] // Confirma que dois produtos com a mesma Referencia são iguais, mesmo com SaldoEstoque diferente
        public void EqualsComMesmaReferenciaESaldoDiferenteTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };
            var produto2 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 999 };

            Assert.IsTrue(produto1.Equals(produto2));
        }

        [Test()] // Confirma que a igualdade por Referencia ignora maiúsculas/minúsculas
        public void EqualsComReferenciaEmCaixaDiferenteTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };
            var produto2 = new EstoqueProduto { Referencia = "a2342", SaldoEstoque = 10 };

            Assert.IsTrue(produto1.Equals(produto2));
            Assert.AreEqual(produto1.GetHashCode(), produto2.GetHashCode());
        }

        [Test()] // Confirma que produtos com Referencia diferente nunca são iguais
        public void EqualsComReferenciaDiferenteTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };
            var produto2 = new EstoqueProduto { Referencia = "B8765", SaldoEstoque = 10 };

            Assert.IsFalse(produto1.Equals(produto2));
        }

        [Test()] // Confirma que comparar com null não lança exceção e retorna false
        public void EqualsComNuloTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };

            Assert.IsFalse(produto1.Equals(null));
        }

        [Test()] // Confirma que o hash code é baseado só na Referencia (consistente com o Equals)
        public void GetHashCodeComMesmaReferenciaTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };
            var produto2 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 999 };

            Assert.AreEqual(produto1.GetHashCode(), produto2.GetHashCode());
        }

        [Test()] // Confirma que o operador == segue a mesma regra do Equals (compara por Referencia)
        public void OperadorIgualdadeComMesmaReferenciaTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };
            var produto2 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 999 };

            Assert.IsTrue(produto1 == produto2);
        }

        [Test()] // Confirma que o operador != detecta Referencia diferente
        public void OperadorDiferencaComReferenciaDiferenteTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };
            var produto2 = new EstoqueProduto { Referencia = "B8765", SaldoEstoque = 10 };

            Assert.IsTrue(produto1 != produto2);
        }

        [Test()] // Confirma que comparar um objeto válido com null via == não lança exceção e retorna false
        public void OperadorIgualdadeComNuloTest()
        {
            var produto1 = new EstoqueProduto { Referencia = "A2342", SaldoEstoque = 10 };
            EstoqueProduto produto2 = null;

            Assert.IsFalse(produto1 == produto2);
        }
    }
}
