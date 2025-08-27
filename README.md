# 🧮 StringCalculator - TDD Kata

> **Um exemplo prático de desenvolvimento orientado por testes (TDD) em C# .NET 9**  
> Demonstrando técnicas avançadas de refatoração, arquitetura limpa e boas práticas de engenharia de software.

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![xUnit](https://img.shields.io/badge/Tests-xUnit-16A085?style=flat-square)](https://xunit.net/)
[![FluentAssertions](https://img.shields.io/badge/Assertions-FluentAssertions-FF6B6B?style=flat-square)](https://fluentassertions.com/)
[![TDD](https://img.shields.io/badge/Development-TDD-4ECDC4?style=flat-square)](#metodologia-tdd)

---

## 📖 Sobre o Projeto

Este projeto implementa o clássico **String Calculator Kata** seguindo rigorosamente a metodologia **Test-Driven Development (TDD)**. É uma demonstração completa de como construir software de qualidade através de ciclos Red-Green-Refactor, com foco em:

- ✅ **Cobertura de testes abrangente** (100% dos cenários de negócio)
- ✅ **Arquitetura limpa** com separação clara de responsabilidades  
- ✅ **Performance otimizada** com processamento single-pass
- ✅ **Tratamento robusto de erros** com mensagens descritivas
- ✅ **Documentação técnica** completa com ADRs (Architecture Decision Records)

### 🎯 Funcionalidades Implementadas

| Funcionalidade | Descrição | Status |
|----------------|-----------|--------|
| **Soma Básica** | Números separados por vírgula: `"1,2,3"` → `6` | ✅ |
| **Delimitadores Múltiplos** | Suporte a vírgula e quebra de linha: `"1\n2,3"` → `6` | ✅ |
| **Delimitadores Customizados** | Header personalizado: `"//;\n1;2"` → `3` | ✅ |
| **Delimitadores Variáveis** | Comprimento variável: `"//[***]\n1***2***3"` → `6` | ✅ |
| **Múltiplos Delimitadores** | Vários ao mesmo tempo: `"//[*][%]\n1*2%3"` → `6` | ✅ |
| **Validação de Negativos** | Rejeita números negativos com lista completa | ✅ |
| **Filtro de Grandes Números** | Ignora números > 1000 na soma | ✅ |
| **Validação Robusta** | Headers malformados geram `FormatException` | ✅ |

---

## 🏗️ Arquitetura & Design

### Estrutura do Projeto
```
StringCalculator/                 # 📦 Biblioteca principal
├── StringCalculator.cs          # 🎯 Classe principal com lógica de negócio
└── StringCalculator.csproj      # 📋 Configuração do projeto

StringCalculator.Test/            # 🧪 Suite de testes
├── Tests.cs                     # ✅ Testes de casos principais
├── InvalidHeaderTests.cs        # ❌ Testes de casos de erro
└── StringCalculator.Test.csproj # 📋 Configuração dos testes

ADR.md                           # 📚 Architecture Decision Records
README.md                        # 📖 Esta documentação
```

### Padrões de Design Aplicados

- **🎯 Single Responsibility**: Cada método tem uma responsabilidade clara
- **🔒 Fail-Fast**: Validações precoces com mensagens descritivas
- **📊 Pipeline Processing**: Fluxo unificado independente da complexidade
- **🚀 Performance Optimization**: Processamento single-pass para eficiência
- **🧪 Test-First Development**: 100% dos recursos desenvolvidos via TDD

---

## 🚀 Como Executar

### Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- IDE compatível (Visual Studio, Rider, VS Code)

### Executando os Testes
```bash
# Clone o repositório
git clone <repository-url>
cd dotnet-tdd-kata

# Restaurar dependências
dotnet restore

# Executar todos os testes
dotnet test

# Executar com cobertura detalhada
dotnet test --verbosity normal
```

### Build do Projeto
```bash
# Build em modo Debug
dotnet build

# Build em modo Release
dotnet build -c Release
```

---

## 🧪 Metodologia TDD

Este projeto demonstra o ciclo completo de **Test-Driven Development**:

### 🔴 **Red Phase**
- Escrever teste que falha
- Definir comportamento esperado
- Garantir que o teste falha pelo motivo correto

### 🟢 **Green Phase**  
- Implementar código mínimo para passar
- Focar em funcionalidade, não em perfeição
- Manter todos os testes passando

### 🔄 **Refactor Phase**
- Melhorar design sem alterar comportamento
- Otimizar performance e legibilidade
- Manter 100% dos testes verdes

### 📊 Exemplo de Commits TDD
```
feat [red]: adicionar teste para números negativos
feat [green]: implementar validação de números negativos  
refactor: otimizar para processamento em passada única
feat [green]: padronizar todos os testes para usar FluentAssertions
```

---

## 🎨 Qualidade de Código

### Métricas de Qualidade
- **📈 Cobertura de Testes**: 100% dos cenários de negócio
- **🔍 Complexidade Ciclomática**: Baixa (métodos focados)
- **📏 Linhas por Método**: < 15 linhas em média
- **🎯 Responsabilidades**: Uma por método/classe

### Ferramentas Utilizadas
- **xUnit** - Framework de testes robusto
- **FluentAssertions** - Asserções expressivas e legíveis
- **Nullable Reference Types** - Segurança de tipos em tempo de compilação
- **ImplicitUsings** - Sintaxe limpa sem using statements repetitivos

### Padrões de Teste
```csharp
[Fact]
public void Given_CustomDelimiter_When_Sum_Then_UsesCorrectDelimiter()
{
    // Arrange
    var calculator = new StringCalculator();
    
    // Act  
    var result = calculator.Sum("//;\n1;2");
    
    // Assert
    result.Should().Be(3);
}
```

---

## 📚 Documentação Técnica

### Architecture Decision Records (ADRs)
O projeto inclui documentação completa das decisões arquiteturais em [`ADR.md`](./ADR.md), cobrindo:

- 🏛️ **Decisões de Arquitetura** - Pipeline unificado vs. bifurcado
- ⚡ **Otimizações de Performance** - Single-pass processing
- 🛡️ **Estratégias de Validação** - Fail-fast vs. collect-and-report
- 🧪 **Filosofia de Testes** - Nomenclatura comportamental

### Highlights Técnicos

#### Processamento Otimizado
```csharp
private static int ProcessNumbers(string[] parts)
{
    var sum = 0;
    var negatives = new List<int>();
    
    foreach (var part in parts)
    {
        var number = int.Parse(part);
        if (number < 0) negatives.Add(number);
        else if (number <= 1000) sum += number;
    }
    
    if (negatives.Count > 0)
        throw new InvalidOperationException($"Negatives not allowed: {string.Join(",", negatives)}");
    
    return sum;
}
```

#### Parsing Robusto de Headers
```csharp
private static (string[] delimiters, string body) ParseHeader(string input)
{
    var nl = input.IndexOf('\n');
    if (nl < 0) throw new FormatException("Invalid header");
    
    var header = input.Substring(2, nl - 2);
    var body = input.Substring(nl + 1);
    
    // Lógica de parsing com validação robusta...
}
```

---

## 🎖️ Destaques para Recrutadores

### 💼 Skills Demonstradas

| Skill | Evidência no Código |
|-------|-------------------|
| **TDD/BDD** | Histórico completo de commits Red-Green-Refactor |
| **Clean Code** | Métodos pequenos, nomes expressivos, responsabilidades claras |
| **SOLID Principles** | SRP evidente, código extensível sem modificação |
| **Performance Optimization** | Refatoração de 3-pass para single-pass processing |
| **Error Handling** | Validações robustas com mensagens descritivas |
| **Documentation** | ADRs completos, README técnico, comentários de código |
| **Testing Strategy** | Separação por concern, nomenclatura comportamental |

### 🏆 Boas Práticas Aplicadas

- **🎯 Domain-Driven Design** - Linguagem ubíqua nos testes e código
- **📊 Continuous Refactoring** - Melhoria contínua sem quebrar funcionalidades  
- **🔒 Defensive Programming** - Validação de entrada e fail-fast
- **📈 Performance Awareness** - Otimizações baseadas em profiling conceitual
- **📚 Technical Documentation** - ADRs documentam decisões e trade-offs

### 💡 Decisões Técnicas de Destaque

1. **Manual Parsing vs Regex** - Escolha consciente por debugging e performance
2. **Unified Pipeline** - Arquitetura consistente independente da complexidade
3. **Comprehensive Error Reporting** - UX melhor através de feedback completo
4. **Single-Pass Optimization** - Trade-off consciente entre simplicidade e performance

---

## 🤝 Contribuições & Feedback

Este projeto foi desenvolvido como demonstração de habilidades técnicas. Feedback sobre:
- Padrões arquiteturais aplicados
- Estratégias de teste utilizadas  
- Decisões de design documentadas
- Qualidade geral do código

São sempre bem-vindos! 

---

## 📄 Licença

Este projeto está sob licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

---

*Desenvolvido com foco em qualidade, performance e manutenibilidade através de Test-Driven Development* 🚀
