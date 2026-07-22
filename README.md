# Desafio de lógica de programação.

A classe [GerenciadorEstoque.cs](https://github.com/grupokyly/TesteDeveloper/blob/master/TesteDeveloper/GerenciadorEstoque.cs) não está completa. 
Precisamos implementar a lógica para alguns métodos de nossa classe de gestão do estoque. Veja os requisitos abaixo:

- [GetSaldo(string referencia)](https://github.com/grupokyly/TesteDeveloper/blob/master/TesteDeveloper/GerenciadorEstoque.cs#L42) - Esse método deve retornar o saldo de estoque da referência
 ```cs
        /// <summary>
        /// Buscar saldo de estoque da referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <returns>Saldo de estoque</returns>
        public int GetSaldo(string referencia)
        {
            //TODO - Implemente sua lógica para buscar e retornar o estoque da referência
            //Dica: Os estoques estão na lista _estoques inicializada no construtor
        }
```


- [EstoqueDisponivel(string referencia, int quantidadeRequerida)](https://github.com/grupokyly/TesteDeveloper/blob/master/TesteDeveloper/GerenciadorEstoque.cs#L31) - Esse método deve retornar verdadeiro se há estoque suficiente para atender a quantidade requerida para referência e falso quando a quantidade de estoque for insuficiente.
```cs
        /// <summary>
        /// Verifica se a quantidade requerida existe no estoque da referência
        /// </summary>
        /// <param name="referencia">Identificador da referência/produto</param>
        /// <param name="quantidadeRequerida">Quantidade requerida</param>
        /// <returns>Indica se a quantidade requerida existe ou não no estoque</returns>
        public bool EstoqueDisponivel(string referencia, int quantidadeRequerida)
        {
            //TODO - Implemente sua lógica para validar o estoque da referência contra a quantidade requerida
            //Dica: Os estoques estão na lista _estoques inicializada no construtor
        }
```

- [ToString()](https://github.com/grupokyly/TesteDeveloper/blob/master/TesteDeveloper/GerenciadorEstoque.cs#L57) - Esse método deve retornar uma string com o extrato do estoque
```cs
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
            //TODO - Implemente sua lógica para formatar uma string no formato esperado
            //Dica: Os estoques estão na lista _estoques inicializada no construtor
        }
```

<hr/>

A implementação deve ser feita na linguagem C#. Para o projeto foi utilizado o [dotnetcore 3.1](https://dotnet.microsoft.com/download)

Clone ou faça download deste repositório e envie a implementação para [rafael@grupokyly.com.br](mailto:rafael@grupokyly.com.br?subject=Teste%20Developer%20Grupo%20Kyly). 

Pode enviar o link do seu próprio repositório (Deixe o repositório público) ou o arquivo compactado com a implementação.

<hr/>

## Implementação

### Métodos implementados

- **`GetSaldo(referencia)`** - busca o produto na lista `_estoques` por `Referencia` e retorna `SaldoEstoque`. Se a referência não existir, lança `KeyNotFoundException` com a mensagem `"Saldo indisponível para o item {referencia}"`.
- **`EstoqueDisponivel(referencia, quantidadeRequerida)`** - reaproveita `GetSaldo` e compara o saldo com a quantidade requerida (`saldo >= quantidadeRequerida`). Por reaproveitar `GetSaldo`, referência inexistente também lança `KeyNotFoundException`.
- **`ToString()`** - formata cada item da lista como `Referência: {Referencia} Saldo: {SaldoEstoque}`, unindo as linhas com `\n` (sem quebra de linha final).

### Decisão de design: referência inexistente

O enunciado original não especifica o comportamento de `GetSaldo`/`EstoqueDisponivel` quando a referência não existe no estoque. Optou-se por lançar `KeyNotFoundException` em vez de retornar um valor padrão (`0`/`false`), para deixar explícito que a referência é inválida em vez de mascarar o erro como "sem saldo".

### Decisão de design: referência nula ou vazia

`GetSaldo`/`EstoqueDisponivel` validam explicitamente se `referencia` é nula ou vazia e lançam `ArgumentException` nesse caso, antes de buscar na lista. Sem essa validação, uma chamada com parâmetro inválido (erro de uso da API) cairia no mesmo `KeyNotFoundException` de "referência não cadastrada" (com a mensagem terminando em branco), misturando dois problemas de natureza diferente: uso incorreto do método vs. referência legítima que não existe no estoque.

### Decisão de design: SaldoEstoque negativo

`EstoqueProduto.SaldoEstoque` valida o valor atribuído e lança `ArgumentOutOfRangeException` para números negativos. Saldo de estoque negativo não tem significado de negócio; zero é permitido (produto cadastrado, sem unidades disponíveis).

### Decisão de design: referência case-insensitive

A busca por `referencia` em `GetSaldo`/`EstoqueDisponivel`, e a igualdade em `EstoqueProduto.Equals`/`GetHashCode`, ignoram maiúsculas/minúsculas (`StringComparison.OrdinalIgnoreCase`). Decisão de domínio: uma referência de produto (ex: `A2342`) representa o mesmo item independente da caixa usada na consulta.

### Testes

Além dos testes originais (com a ordem dos argumentos de `Assert.AreEqual` corrigida para `(expected, actual)`), foram adicionados:

- Casos de referência inexistente em `GetSaldo` e `EstoqueDisponivel`
- Casos de referência nula ou vazia em `GetSaldo` e `EstoqueDisponivel`
- Construtor com lista nula
- Casos-limite de `EstoqueDisponivel` (quantidade requerida igual a 0 e negativa)
- Busca por referência ignorando maiúsculas/minúsculas
- `ToString()` com lista vazia
- `EstoqueProdutoTests.cs` (novo arquivo) - cobre `Equals`, `GetHashCode` e os operadores `==`/`!=` de `EstoqueProduto` (incluindo o comportamento de igualdade baseado apenas em `Referencia`), além da validação de `SaldoEstoque` negativo

### Infraestrutura

- `TargetFramework` migrado de `netcoreapp3.1` (fora de suporte desde dez/2022) para `net10.0`, a versão LTS instalada no ambiente de desenvolvimento.
- Pacotes de teste (`Microsoft.NET.Test.Sdk`, `MSTest.*`, `NUnit`, `NUnit3TestAdapter`, `coverlet.collector`) atualizados para eliminar uma vulnerabilidade conhecida (NU1903) numa dependência transitiva (`Newtonsoft.Json` 9.0.1).
