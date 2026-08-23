using Coinly.Filtros;
using Coinly.Modelos;

namespace Coinly.Test
{
    [Collection("Console")]
    public class LinqFilterFiltrarValores
    {
        [Fact]
        public void EstouraArgumentNullExceptionQuandoListaNull()
        {
            //a
            Assert.Throws<ArgumentNullException>(() => LinqFilter.FiltrarValores(null, "BTC"));
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
            LinqFilter.FiltrarValores(cotacoes, "BTC");
            string resposta = stringWriter.ToString();

            //a
            Assert.Contains("Não existe cotações feita da BTC", resposta);
        }

        [Fact]
        public void CalculaMaxMinMediaEVariacaoCorretamente()
        {
            //a
            var cotacoes = new List<Cotacao>
            {
                new Cotacao { Sigla = "BTC", Timestamp = 100, Valor = 1000, DataHora = "2026-06-20" },
                new Cotacao { Sigla = "BTC", Timestamp = 300, Valor = 3000, DataHora = "2026-06-25" },
                new Cotacao { Sigla = "BTC", Timestamp = 200, Valor = 2000, DataHora = "2026-06-22" },
            };

            var resumoEsperado = "\nResumo BTC\n";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            LinqFilter.FiltrarValores(cotacoes, "BTC");
            string resposta = stringWriter.ToString();

            //a
            Assert.Contains(resumoEsperado, resposta);
            Assert.Contains("Maior cotação já resgistrada: 3000", resposta);
            Assert.Contains("Menor cotação já resgistrada: 1000", resposta);
            Assert.Contains("Valor Medio das cotações: 2000", resposta);
            Assert.Contains("Variação entre primeira e ultima cotação: 2000", resposta);
        }
    }
}
