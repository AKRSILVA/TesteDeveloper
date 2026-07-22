using System;
using System.Collections.Generic;

namespace TesteDeveloper
{
    /// <summary>
    /// Estrutura para saldo de estoque da referência
    /// </summary>
    public class EstoqueProduto : IEquatable<EstoqueProduto> // IEquatable comparara a classe com outra classe do mesmo tipo, para verificar se são iguais
    {
        /// <summary>
        /// Identificador da referência/produto
        /// </summary>
        public string Referencia { get; set; }

        private int _saldoEstoque;

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Lançada ao atribuir um valor negativo</exception>
        public int SaldoEstoque
        {
            get => _saldoEstoque;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "SaldoEstoque não pode ser negativo");

                _saldoEstoque = value;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as EstoqueProduto); // Chama o método Equals da interface IEquatable
        }

        public bool Equals(EstoqueProduto other)
        {
            // Case-insensitive: referência de produto é a mesma independente de maiúsculas/minúsculas
            return other != null &&
                   string.Equals(Referencia, other.Referencia, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Referencia?.ToUpperInvariant());
        }

        public static bool operator ==(EstoqueProduto left, EstoqueProduto right)
        {
            return EqualityComparer<EstoqueProduto>.Default.Equals(left, right);
        }

        public static bool operator !=(EstoqueProduto left, EstoqueProduto right)
        {
            return !(left == right);
        }
    }
}