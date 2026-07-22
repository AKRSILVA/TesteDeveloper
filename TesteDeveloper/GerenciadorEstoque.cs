using System;
using System.Collections.Generic;
using System.Linq;

namespace TesteDeveloper
{
    /// <summary>
    /// Implementação da administração de estoque
    /// </summary>
    public class GerenciadorEstoque
    {
        //Saldos de estoque por referência
        private readonly IList<EstoqueProduto> _estoques;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="estoques">Saldos de estoque por referência</param>
        public GerenciadorEstoque(IList<EstoqueProduto> estoques)
        {
            _estoques = estoques ?? throw new ArgumentNullException(nameof(estoques));
        }

        /// <summary>
        /// Verifica se a quantidade requerida existe no estoque da referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <param name="quantidadeRequerida">Quantidade requerida</param>
        /// <returns>Indica se a quantidade requerida existe ou não no estoque</returns>
        /// <exception cref="ArgumentException">Lançada quando <paramref name="referencia"/> é nula ou vazia</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando <paramref name="referencia"/> não está cadastrada no estoque</exception>
        public bool EstoqueDisponivel(string referencia, int quantidadeRequerida)
        {
            return GetSaldo(referencia) >= quantidadeRequerida;
        }

        /// <summary>
        /// Buscar saldo de estoque da referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <returns>Saldo de estoque</returns>
        /// <exception cref="ArgumentException">Lançada quando <paramref name="referencia"/> é nula ou vazia</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando <paramref name="referencia"/> não está cadastrada no estoque</exception>
        public int GetSaldo(string referencia)
        {
            // Validação explícita evita que referência nula/vazia caia direto no KeyNotFoundException
            // (uso incorreto da API deve ser distinguido de "referência legitimamente não encontrada")
            if (string.IsNullOrEmpty(referencia))
                throw new ArgumentException("Referência não pode ser nula ou vazia", nameof(referencia));

            // Comparação case-insensitive: referência de produto é tratada como o mesmo item independente de maiúsculas/minúsculas
            var estoqueProduto = _estoques.FirstOrDefault(e => string.Equals(e.Referencia, referencia, StringComparison.OrdinalIgnoreCase));

            if (estoqueProduto == null)
                throw new KeyNotFoundException($"Saldo indisponível para o item {referencia}");

            return estoqueProduto.SaldoEstoque;
        }


        /// <summary>
        /// Gera string com os estoques no formato [Referência: {Referencia} Saldo: {SaldoEstoque}] com uma linha para cada referência
        /// Ex: 
        /// Referência: A345 Saldo: 98
        /// Referência: B456 Saldo: 15
        /// 
        /// </summary>
        /// <returns>String formatada</returns>
        public override string ToString()
        {
            return string.Join("\n", _estoques.Select(e => $"Referência: {e.Referencia} Saldo: {e.SaldoEstoque}"));
        }


    }
}
