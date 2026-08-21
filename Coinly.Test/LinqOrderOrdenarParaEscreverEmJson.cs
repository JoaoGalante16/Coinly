using Bogus;
using Coinly.Filtros;
using Coinly.Modelos;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;


//Input null → retorna null
//Lista vazia (new List<Cotacao>()) → retorna lista vazia, sem estourar exceção
//Agrupamento por Sigla — monta cotações de 2 moedas diferentes (ex: 2 de BTC, 1 de ETH) → confere que saem 2 grupos, com os Sigla certos
//Ordenação por Timestamp decrescente — dentro de um mesmo Sigla, cria cotações com Timestamps fora de ordem (ex: 100, 300, 200) → confere que saem ordenadas decrescente (300, 200, 100)
//Total desbalanceado — grupos de tamanhos diferentes (ex: 3 de BTC, 1 de ETH) → confere que Total de BTC é 3 e de ETH é 1, não a contagem da lista inteira

namespace Coinly.Test
{
    public class LinqOrderOrdenarParaEscreverEmJson
    {
        [Fact]
        public void RetornaNullQuandoEntradaNull()
        {


            //a
            var resposta = LinqOrder.OrdenarParaEscreverEmJson(null);

            //a
            Assert.Null(resposta);
        }

        [Fact]
        public void RetornaListaVaziaQuandoListaVazia()
        {
            //a
            var lista = new List<Cotacao>();

            //a
            var resposta = LinqOrder.OrdenarParaEscreverEmJson(lista);

            //a
            Assert.Empty(resposta);
        }

        [Fact]
        public void RetornaQuantidadeDeGruposQuandoOrdenarParaEscreverEmJson()
        {
            //a
            var fakercotacaoBTC = new Faker<Cotacao>().CustomInstantiator(faker =>
            new Cotacao()
            {
                Sigla = "BTC",
                DataHora = "2020-08-26",
                Valor = faker.Random.Double(300, 4000),
                Timestamp = faker.Random.Int()
            });

            var fakercotacaousd = new Faker<Cotacao>().CustomInstantiator(faker =>
            new Cotacao()
            {
                Sigla = "USD",
                DataHora = "2020-08-26",
                Valor = faker.Random.Double(300, 4000),
                Timestamp = faker.Random.Int()
            }

            );
            var cotacoes = new List<Cotacao>(fakercotacaoBTC.Generate(10));
            cotacoes.AddRange(fakercotacaousd.Generate(15));
            var quantidadeGrupo = cotacoes.Select(c => c.Sigla).Distinct().Count();

            //a
            var resultado = LinqOrder.OrdenarParaEscreverEmJson(cotacoes);

            //a

            Assert.Equal(quantidadeGrupo, resultado.Count);
        }

        [Fact]
        public void RetornaTimestampOrdenadoEmOrderDescrescente()
        {
            //a
            var fakercotacaoBTC = new Faker<Cotacao>().CustomInstantiator(faker =>
            new Cotacao()
            {
                Sigla = "BTC",
                DataHora = "2020-08-26",
                Valor = faker.Random.Double(300, 4000),
                Timestamp = faker.Random.Int()
            });

            var fakercotacaousd = new Faker<Cotacao>().CustomInstantiator(faker =>
            new Cotacao()
            {
                Sigla = "USD",
                DataHora = "2020-08-26",
                Valor = faker.Random.Double(300, 4000),
                Timestamp = faker.Random.Int()
            }

            );
            var cotacoes = new List<Cotacao>(fakercotacaoBTC.Generate(10));
            cotacoes.AddRange(fakercotacaousd.Generate(15));
            var resposta = LinqOrder.OrdenarParaEscreverEmJson(cotacoes);

  
            var timestampUSD = resposta.Single(g => g.Sigla == "USD").Cotacoes.Select(c => c.Timestamp).ToList();
            var timestampOrdenado = timestampUSD.OrderByDescending(t => t).ToList();

            //a

            Assert.Equal(timestampOrdenado, timestampUSD);
        }

        [Fact]
        public void RetornaTotalDeMoedasPorGrupo()
        {
            //a
            var fakercotacaoBTC = new Faker<Cotacao>().CustomInstantiator(faker =>
            new Cotacao()
            {
                Sigla = "BTC",
                DataHora = "2020-08-26",
                Valor = faker.Random.Double(300, 4000),
                Timestamp = faker.Random.Int()
            });

            var fakercotacaousd = new Faker<Cotacao>().CustomInstantiator(faker =>
            new Cotacao()
            {
                Sigla = "USD",
                DataHora = "2020-08-26",
                Valor = faker.Random.Double(300, 4000),
                Timestamp = faker.Random.Int()
            }

            );
            var cotacoes = new List<Cotacao>(fakercotacaoBTC.Generate(10));
            cotacoes.AddRange(fakercotacaousd.Generate(15));
            var resultado = LinqOrder.OrdenarParaEscreverEmJson(cotacoes);

            //a
            var grupoBTC = resultado.Single(g => g.Sigla == "BTC");
            var grupoUSD = resultado.Single(g => g.Sigla == "USD");

            //a
            Assert.Equal(15, grupoUSD.Total);
            Assert.Equal(10, grupoBTC.Total);
        }


    }
}
