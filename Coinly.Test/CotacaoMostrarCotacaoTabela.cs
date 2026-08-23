using Coinly.Modelos;

namespace Coinly.Test
{
    [Collection("Console")]
    public class CotacaoMostrarCotacaoTabela
    {
        [Fact]
        public void ImprimeCabecalhoComFormatoCorreto()
        {
            //a
            var cabecalhoEsperado = $"{"Moeda",-10}{"Valor",-15}{"Data",-15}\r\n";

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //a
            Cotacao.MostrarCotacaoTabela();
            string resposta = stringWriter.ToString();

            //a
            Assert.Equal(cabecalhoEsperado, resposta);
        }
    }
}
