using Bogus;
using Coinly.Modelos;
using Coinly.Filtros;

namespace Coinly.Test
{
    [Collection("Console")]
    public class LinqOrderOrdenarPorMoedas
    {
        [Fact]
        public void NaoImprimeNadaQuandoEntradaNull()
        {
            //a
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqOrder.OrdenarPorMoedas(null);
            string resposta = stringWriter.ToString();

            //a
            Assert.Empty(resposta);
        }

        [Fact]
        public void NaoImprimeNadaQuandoListaVazia()
        {
            //a
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var lista = new List<Cotacao>();

            //a
            LinqOrder.OrdenarPorMoedas(lista);
            string resposta = stringWriter.ToString();

            //a
            Assert.Empty(resposta);
        }

        [Fact]
        public void ImprimeUmBlocoPorMoedaDistinta()
        {
            //a
            var fakerCotacaoBTC = new Faker<Cotacao>().CustomInstantiator(faker =>
                new Cotacao
                {
                    Sigla = "BTC",
                    DataHora = "2020-08-26",
                    Valor = faker.Random.Double(300, 4000),
                    Timestamp = faker.Random.Int()
                });
            var fakerCotacaoUSD = new Faker<Cotacao>().CustomInstantiator(faker =>
                new Cotacao 
                {
                    Sigla = "USD", 
                    DataHora = "2020-08-26", 
                    Valor = faker.Random.Double(300, 4000), 
                    Timestamp = faker.Random.Int() 
                });

            var cotacoes = new List<Cotacao>(fakerCotacaoBTC.Generate(3));
            cotacoes.AddRange(fakerCotacaoUSD.Generate(2));

            var quantidadeGrupos = cotacoes.Select(c => c.Sigla).Distinct().Count();
            var borda = "".PadRight(50, '=');

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqOrder.OrdenarPorMoedas(cotacoes);
            string resposta = stringWriter.ToString();

            //a
            var quantidadeBlocosImpressos = resposta.Split(borda).Length - 1;
            Assert.Equal(quantidadeGrupos, quantidadeBlocosImpressos);
        }

        [Fact]
        public void OrdenaCotacoesPorTimestampDecrescenteDentroDoBloco()
        {
            var cotacoes = new List<Cotacao>
            {
                new Cotacao { Sigla = "BTC", Timestamp = 100, Valor = 1.0, DataHora = "2026-06-20" },
                new Cotacao { Sigla = "BTC", Timestamp = 300, Valor = 3.0, DataHora = "2026-06-25" },
                new Cotacao { Sigla = "BTC", Timestamp = 200, Valor = 2.0, DataHora = "2026-06-22" },
            };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqOrder.OrdenarPorMoedas(cotacoes);
            string resposta = stringWriter.ToString();


            //a
            var posicaoMaisRecente = resposta.IndexOf("2026-06-25");
            var posicaoIntermediaria = resposta.IndexOf("2026-06-22");
            var posicaoMaisAntiga = resposta.IndexOf("2026-06-20");
            Assert.True(posicaoMaisRecente < posicaoIntermediaria);
            Assert.True(posicaoIntermediaria < posicaoMaisAntiga);
        }

        [Fact]
        public void ImprimeBlocosOrdenadosPorSiglaCrescente()
        {
            //a
            var cotacoes = new List<Cotacao>
            {
                new Cotacao { Sigla = "USD", Timestamp = 100, Valor = 1.0, DataHora = "2026-06-20" },
                new Cotacao { Sigla = "BTC", Timestamp = 100, Valor = 1.0, DataHora = "2026-06-20" },
            };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqOrder.OrdenarPorMoedas(cotacoes);
            string resposta = stringWriter.ToString();

            //a
            var posicaoBTC = resposta.IndexOf("BTC");
            var posicaoUSD = resposta.IndexOf("USD");

            Assert.True(posicaoBTC < posicaoUSD);
        }

        [Fact]
        public void ImprimeCabecalhoDaTabelaQuandoExisteCotacao()
        {
            //a
            var cotacoes = new List<Cotacao>
            {
                new Cotacao { Sigla = "BTC", Timestamp = 100, Valor = 1.0, DataHora = "2026-06-20" },
            };

            var cabecalhoEsperado = $"{"Moeda",-10}{"Valor",-15}{"Data",-15}";

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqOrder.OrdenarPorMoedas(cotacoes);
            string resposta = stringWriter.ToString();

            //a
            Assert.Contains(cabecalhoEsperado, resposta);
        }
    }
}
