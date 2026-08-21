using Coinly.Filtros;
using Coinly.Modelos;

namespace Coinly.Test
{
    [Collection("Console")]
    public class LinqFilterFiltrarMoedaValor
    {
        [Fact]
        public void EstouraArgumentNullExceptionQuandoListaNull()
        {
            //a
            Assert.Throws<ArgumentNullException>(() => LinqFilter.FiltrarMoedaValor(null, "BTC"));
        }

        [Fact]
        public void ImprimeMensagemQuandoMoedaNaoEncontrada()
        {
            //a
            var cotacoes = new List<Cotacao>
            {
                new Cotacao { Sigla = "ETH", Timestamp = 100, Valor = 1000, DataHora = "2026-06-20" }
            };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqFilter.FiltrarMoedaValor(cotacoes, "BTC");
            string resposta = stringWriter.ToString();

            //a
            Assert.Contains("Não existe cotações feita da BTC", resposta);
        }

        [Fact]
        public void OrdenaCotacoesPorValorDecrescenteQuandoMoedaEncontrada()
        {
            //a
            var cotacoes = new List<Cotacao>
            {
                new Cotacao { Sigla = "BTC", Timestamp = 100, Valor = 1000, DataHora = "2026-06-20" },
                new Cotacao { Sigla = "BTC", Timestamp = 300, Valor = 3000, DataHora = "2026-06-25" },
                new Cotacao { Sigla = "BTC", Timestamp = 200, Valor = 2000, DataHora = "2026-06-22" },
            };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqFilter.FiltrarMoedaValor(cotacoes, "BTC");
            string resposta = stringWriter.ToString();

            //a
            var posicaoMaiorValor = resposta.IndexOf("2026-06-25");
            var posicaoValorIntermediario = resposta.IndexOf("2026-06-22");
            var posicaoMenorValor = resposta.IndexOf("2026-06-20");

            Assert.True(posicaoMaiorValor < posicaoValorIntermediario);
            Assert.True(posicaoValorIntermediario < posicaoMenorValor);
        }
    }
}
