# console-selenium-42-net

Atividade de Garantia da Qualidade de Software: automacao de testes end-to-end no
[DuckDuckGo](https://duckduckgo.com) com .NET 10, MSTest e Selenium WebDriver.

## Cenarios automatizados

| Exercicio | Teste | O que valida |
|---|---|---|
| 1 | `Exercicio1_PesquisaBasica_DeveValidarResultados` | Pesquisa por "Selenium WebDriver C#" na barra principal e carregamento da pagina de resultados |
| 2 | `Exercicio2_Autocompletar_DeveSelecionarTerceiraSugestao` | Sugestoes dinamicas com `WebDriverWait` e clique na terceira opcao |
| 3 | `Exercicio3_Historico_DeveVoltarParaResultados` | Clique no primeiro link organico, validacao da URL externa e retorno aos resultados com `Navigate().Back()` |

## Estrutura da solution

```
ConsoleSelenium42.sln
ConsoleSelenium/                # Console Application (.NET 10)
ConsoleSelenium.Tests/          # Projeto MSTest com os cenarios Selenium
```

## Pre-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- Google Chrome instalado
- Conexao com a internet

O ChromeDriver e gerenciado automaticamente pelo Selenium Manager; nao e necessario
instalar o driver manualmente.

## Como executar

Restaurar e compilar:

```powershell
dotnet build ConsoleSelenium42.sln
```

Executar todos os testes:

```powershell
dotnet test ConsoleSelenium42.sln
```

Executar um cenario especifico:

```powershell
dotnet test ConsoleSelenium42.sln --filter "FullyQualifiedName~Exercicio1"
```

Os testes abrem o Chrome em modo visivel e encerram o navegador ao final de cada cenario.

## Tecnologias

- .NET 10
- MSTest 4
- Selenium WebDriver 4 (`Selenium.WebDriver` e `Selenium.Support`)

## Observacoes

Os cenarios dependem do DuckDuckGo em tempo real. Eventuais bloqueios de bot ou
indisponibilidade de rede podem exigir nova execucao.
