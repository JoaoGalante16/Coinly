using Bogus;
using Coinly.Filtros;
using Coinly.Modelos;

namespace Coinly.Test
{
    [Collection("Console")]
    public class LinqFilterFiltrarMoedaMaisCotada
    {
        [Fact]
        public void EstouraArgumentNullExceptionQuandoListaNull()
        {
            //a
            Assert.Throws<ArgumentNullException>(() => LinqFilter.FiltrarMoedaMaisCotada(null));
        }

        [Fact]
        public void NaoImprimeNadaQuandoListaVazia()
        {
            //a
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var lista = new List<Cotacao>();

            //a
            LinqFilter.FiltrarMoedaMaisCotada(lista);
            string resposta = stringWriter.ToString();

            //a
            Assert.Empty(resposta);
        }

        [Fact]
        public void EscolheMoedaComMaisRegistrosQuandoDesbalanceado()
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
            cotacoes.AddRange(fakerCotacaoUSD.Generate(1));

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqFilter.FiltrarMoedaMaisCotada(cotacoes);
            string resposta = stringWriter.ToString();

            //a
            Assert.Contains("A moeda mais cotada é BTC com 3 cotações", resposta);
        }
    }
}
