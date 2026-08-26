using Bogus;
using Coinly.Modelos;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using Coinly.Filtros;

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
