# ADR — StringCalculator (dotnet-tdd-kata)

> "Registros de Decisão Arquitetural documentando escolhas técnicas feitas durante desenvolvimento TDD."

---

## Resumo Executivo

**Aceitas**: (1) Estratégia de normalização `\n→,` com `Split(',')` para delimitadores padrão; (2) Validação abrangente de números negativos com mensagens de exceção detalhadas; (3) Filtragem de regra de negócio `>1000` no estágio de somatória; (4) Headers de delimitadores customizados suportando char único `//;`, comprimento variável `//[***]`, e múltiplos `//[*][%]` com isolamento estrito dos padrões; (5) Parsing manual com métodos auxiliares focados ao invés de regex; (6) Pipeline de processamento unificado; (7) Validação fail-fast para headers customizados malformados; (8) Otimização de processamento de números em passada única; (9) Estratégia consistente de asserções de teste com FluentAssertions.

**Rejeitadas**: R01 abordagem regex única; R02 processamento bifurcado single/multi; R03 modos de delimitadores mistos; R04 correção silenciosa de header; R05 falhar no primeiro negativo; R06 incluir >1000 na soma; R07 parsing específico de cultura; R08 extração de colchetes baseada em regex; R09 normalização automática de espaços em branco.

---

## Log Cronológico de Decisões

### Fase 1 — Fundação & Delimitadores Padrão

**ADR-001 — Estratégia de Delimitadores Padrão**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Etapas 1-3 do Kata  
**Decisão:** Implementar abordagem normalização-primeiro: `input.Replace('\n', ',').Split(',', StringSplitOptions.RemoveEmptyEntries)`  
**Justificativa:** Reduz complexidade cognitiva tratando todas as entradas como separadas por vírgula após normalização. Evita overhead de regex para casos simples.  
**Consequências:** + Modelo mental simples; + Alta performance; - Leve overhead de alocação de string  
**Alternativas consideradas:** Split multi-delimitador direto, pattern matching com regex

**ADR-002 — Pipeline de Processamento Unificado**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Arquitetura  
**Decisão:** Pipeline linear único independente da complexidade da entrada: detectar header → dividir → parsear → validar → somar  
**Justificativa:** Elimina complexidade de lógica de ramificação, melhora testabilidade e manutenibilidade  
**Consequências:** + Comportamento consistente; + Debug mais fácil; - Leve custo de performance para casos simples

---

### Fase 2 — Delimitadores Customizados & Parsing de Header

**ADR-003 — Protocolo de Header de Delimitador Customizado**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Etapas 4, 7-8 do Kata  
**Decisão:** 
- Detectar modo customizado via `StartsWith("//")`
- Parsear divisão header/body na primeira ocorrência de `\n`
- Char único: `//;\n` (validar length == 1)
- Multi-char: `//[delimitador]\n` (extrair conteúdo dos colchetes)
- Múltiplos: `//[del1][del2]\n` (extrair todos os pares de colchetes)
- **Isolamento estrito**: modo customizado ignora delimitadores padrão (`,` e `\n`)

**Justificativa:** Separação clara de protocolo previne ambiguidade. Parsing manual fornece melhores mensagens de erro que regex.  
**Consequências:** + Comportamento previsível; + Bom tratamento de erros; - Mais código de parsing  
**Alternativas consideradas:** Extração baseada em regex, parsing de modo misto

**ADR-004 — Validação Fail-Fast de Header**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Robustez  
**Decisão:** Lançar `FormatException` para headers malformados:
- Newline faltando após header
- Colchetes não fechados
- Colchetes de delimitador vazios `[]`
- Multi-char sem colchetes `//ab\n`

**Justificativa:** Melhor experiência do desenvolvedor através de mensagens de erro precoces e claras  
**Consequências:** + Contrato claro; + Debug fácil; - Mais código de validação

---

### Fase 3 — Regras de Negócio & Validação

**ADR-005 — Tratamento Abrangente de Números Negativos**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Etapa 5 do Kata  
**Decisão:** Coletar todos os números negativos durante parsing, lançar única `InvalidOperationException` com lista completa: `"Negatives not allowed: -2,-5"`  
**Justificativa:** Fornece feedback completo em operação única, melhor DX que falhar no primeiro  
**Consequências:** + Informação de erro completa; + Passada única de validação; - Requer lógica de coleta

**ADR-006 — Estratégia de Filtragem de Regra de Negócio**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Etapa 6 do Kata  
**Decisão:** Aplicar filtragem `>1000` durante estágio de somatória: `if (n <= 1000) sum += n`  
**Justificativa:** Mantém regras de negócio colocadas, separa parsing de lógica de negócio  
**Consequências:** + Separação clara de responsabilidades; + Regras de negócio em um lugar

---

### Fase 4 — Arquitetura de Implementação

**ADR-007 — Parsing Manual com Auxiliares Focados**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Estratégia de implementação  
**Decisão:** Implementar parsing através de métodos privados pequenos e focados:
- `StartsWithHeader()` — detecção de header
- `ParseHeader()` — extração de delimitador
- `ExtractBracketedDelimiters()` — parsing de colchetes
- `SplitDefault()` — tratamento de delimitador padrão

**Justificativa:** Melhor testabilidade, legibilidade e debug que regex monolítica  
**Consequências:** + Design modular; + Teste unitário fácil; + Responsabilidades claras; - Mais overhead de métodos

---

### Fase 5 — Otimizações de Performance & Qualidade de Código

**ADR-008 — Processamento de Números em Passada Única**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Otimização de performance  
**Decisão:** Substituir processamento de três passadas (`ToInts()` → `ThrowIfNegatives()` → `SumUpTo1000()`) com método único `ProcessNumbers()` combinando parsing, validação e somatória  
**Justificativa:** Reduz alocações de memória e melhora performance para entradas grandes  
**Consequências:** + Melhor performance; + Redução de pressão de memória; - Método único ligeiramente mais complexo  
**Migração:** Refatorado durante fase green, manteve todos os contratos de teste existentes

**ADR-009 — Estratégia Padronizada de Asserção de Testes**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Manutenibilidade de testes  
**Decisão:** Migrar todas as asserções de teste para sintaxe FluentAssertions:
- `Assert.Equal(expected, actual)` → `actual.Should().Be(expected)`
- `Assert.Throws<T>()` → `act.Should().Throw<T>()`

**Justificativa:** Sintaxe de asserção consistente melhora legibilidade e fornece melhores mensagens de falha  
**Consequências:** + Estilo de teste consistente; + Melhores mensagens de erro; + Legibilidade melhorada

---

### Fase 6 — Filosofia de Design de Testes

**ADR-010 — Convenção de Nomenclatura de Testes Comportamentais**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Design de testes  
**Decisão:** Nomes de métodos de teste descrevem comportamento ao invés de valores literais: padrão `Given_X_When_Y_Then_Z` com foco comportamental  
**Justificativa:** Testes servem como documentação viva do comportamento do sistema  
**Consequências:** + Testes auto-documentados; + Foco em comportamento sobre implementação

**ADR-011 — Organização de Testes por Responsabilidade**  
**Status:** ✅ Aceita · **Data:** 2025-08-27 · **Contexto:** Arquitetura de testes  
**Decisão:** Arquivos de teste separados por responsabilidade: `Tests.cs` para caminhos felizes, `InvalidHeaderTests.cs` para casos de erro  
**Justificativa:** Melhora descoberta de testes e manutenção  
**Consequências:** + Organização clara de testes; + Fácil encontrar tipos específicos de teste

---

## Alternativas Rejeitadas

### Arquitetura Técnica
**ADR-R01 — Solução Regex Única**: Rejeitada devido a preocupações de manutenibilidade e dificuldade de debug  
**ADR-R02 — Pipeline de Processamento Bifurcado**: Rejeitada em favor de abordagem unificada para consistência  
**ADR-R05 — Parsing de Colchetes Baseado em Regex**: Parsing manual preferido para debug e controle

### Lógica de Negócio  
**ADR-R03 — Modos de Delimitadores Mistos**: Rejeitada para prevenir ambiguidade no comportamento de delimitadores customizados  
**ADR-R04 — Falhar no Primeiro Negativo**: Rejeitada em favor de relatório de erro abrangente  
**ADR-R06 — Incluir >1000 na Soma**: Contradiz requisitos do kata

### Detalhes de Implementação
**ADR-R07 — Correção Silenciosa de Header**: Fail-fast preferido para melhor experiência do desenvolvedor  
**ADR-R08 — Parsing de Número Específico de Cultura**: Fora do escopo do kata  
**ADR-R09 — Tratamento Automático de Espaços em Branco**: Fora do escopo do kata, adiciona complexidade

---

## Matriz de Impacto de Decisões

| ADR | Performance | Manutenibilidade | Testabilidade | Experiência do Usuário |
|-----|------------|------------------|---------------|------------------------|
| 001 | ✅ Alta    | ✅ Alta          | ✅ Alta       | ✅ Previsível         |
| 002 | ⚠️ Neutro  | ✅ Alta          | ✅ Alta       | ✅ Consistente        |
| 003 | ⚠️ Neutro  | ✅ Alta          | ✅ Alta       | ✅ Regras Claras      |
| 004 | ✅ Fail Rápido| ✅ Alta        | ✅ Alta       | ✅ Erros Claros       |
| 005 | ⚠️ Neutro  | ✅ Alta          | ✅ Alta       | ✅ Info Completa      |
| 006 | ✅ Alta    | ✅ Alta          | ✅ Alta       | ✅ Lógica Clara       |
| 007 | ⚠️ Neutro  | ✅ Muito Alta    | ✅ Muito Alta | ⚠️ Neutro            |
| 008 | ✅ Muito Alta| ✅ Alta        | ✅ Alta       | ⚠️ Neutro            |
| 009 | ⚠️ Neutro  | ✅ Muito Alta    | ✅ Alta       | ✅ Melhor DX         |

---

*Última atualização: 2025-08-27 | Próxima revisão: Conforme necessário durante evolução do kata*

---

*NOTA: Este documento ADR foi estruturado com auxílio do Claude (Anthropic), que contribuiu com a organização técnica das decisões, linguagem arquitetural apropriada e formatação seguindo padrões de documentação de decisões arquiteturais. As decisões técnicas documentadas foram todas tomadas durante o desenvolvimento TDD do projeto.*
