# Coinly

```
   ____      _       _
  / ___|___ (_)_ __ | |_   _
 | |   / _ \| | '_ \| | | | |
 | |__| (_) | | | | | | |_| |
  \____\___/|_|_| |_|_|\__, |
                       |___/
```

Aplicação de console em C# para cotar moedas e criptomoedas em tempo real, guardar o histórico de consultas e filtrar/analisar os dados salvos.

## Sobre o projeto

Coinly é um projeto de estudo que estou construindo e aprimorando conforme avanço nos meus estudos de C# (regex, LINQ, async/await, consumo de API, manipulação de arquivos). A ideia é que cada funcionalidade reflita um conceito que eu já entendi de verdade, não só copiado — então o projeto vai continuar evoluindo com o tempo.

## Funcionalidades

- **Cotar moeda(s)**: consulta uma ou várias siglas de uma vez (ex: `BTC, USD, ETH`), com validação de entrada via Regex
- **Histórico de cotações**: toda cotação consultada é salva em um arquivo CSV local
- **Consultar histórico por moeda**:
  - Ordenado por valor
  - Ordenado por data
  - Resumo estatístico (maior, menor, média e variação entre a primeira e a última cotação)
- **Visão geral do histórico**: todas as cotações agrupadas por moeda, e qual moeda foi mais consultada
- **Exportar cotações em JSON**: gera um arquivo `Cotacoes.json` formatado (indentado) com as cotações agrupadas por moeda, cada uma com seu histórico ordenado e a contagem de consultas

## Tecnologias

- C# / .NET 10
- `HttpClient` para consumo de API REST
- `System.Text.Json` para desserialização das respostas
- `System.Text.RegularExpressions` para validação e extração de entrada do usuário
- LINQ para filtros, ordenação e agregações sobre o histórico (`GroupBy`, projeções, ordenações)
- Leitura/escrita de arquivo CSV (`StreamReader` / `StreamWriter`)
- `Environment.SpecialFolder` para guardar os dados em um caminho fixo e confiável, independente de onde o programa é executado

## API utilizada

As cotações são obtidas via [AwesomeAPI](https://docs.awesomeapi.com.br/api-de-moedas), um serviço público de cotações de moedas e criptomoedas.

## Onde os dados ficam salvos

- **Histórico (CSV)**: `%APPDATA%\Coinly\Cotacoes.csv` — sempre no mesmo lugar, não importa de onde o programa é executado
- **Exportação (JSON)**: gerada na sua Área de Trabalho, como `Cotacoes.json`

## Como rodar

Pré-requisito: [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado.

```bash
git clone https://github.com/JoaoGalante16/Coinly.git
cd Coinly/Coinly
dotnet run
```

## Estrutura do projeto

```
Coinly/
├── Chamadas/     # Chamadas HTTP para a API de cotações e moedas
├── Filtros/      # Filtros e ordenações LINQ sobre o histórico
├── Funções/      # Leitura e escrita do arquivo CSV de histórico
├── Menus/        # Fluxo de navegação do console
├── Modelos/      # Modelos de dados (Cotacao, MoedaAgrupada)
├── Services/      # Regras de negócio da consulta de cotação
└── Program.cs    # Ponto de entrada
```
