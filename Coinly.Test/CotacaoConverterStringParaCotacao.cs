using Coinly.Modelos;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace Coinly.Test
{
    public class CotacaoConverterStringParaCotacao
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void RetornaNullQuandoLinhaVaziaOuNull(string linha)
        {
            //a
            var resposta = Cotacao.ConverterStringParaCotacao(linha);
            //a
            Assert.Null(resposta);
        }

        [Fact]
        public void ValidaConverterStringParaCotacaoQuandoDadosValidosEFormatados()
        {
            //a
            var linha = "BTC,5000.00,2026-08-26,123456878612345";
            var esperadoSigla = "BTC";
            var esperadoValor = 5000.0;
            var esperadoDataHora = "2026-08-26";
            var esperadoTimestamp = 123456878612345;

            //a
            var resposta = Cotacao.ConverterStringParaCotacao(linha);
            //a
            Assert.Equal(esperadoSigla, resposta.Sigla);
            Assert.Equal(esperadoValor, resposta.Valor);
            Assert.Equal(esperadoDataHora, resposta.DataHora);
            Assert.Equal(esperadoTimestamp, resposta.Timestamp);
        }

        [Fact]
        public void RetornaIndexOutOfRangeExceptionQuandoLinhaComCampoFaltante()
        {
            //a
            var linha = "BTC,5000.00,123456878612345";

            //a
            Assert.Throws<IndexOutOfRangeException>(() => Cotacao.ConverterStringParaCotacao(linha));
        }

        [Fact]
        public void RetonarFormatExceptionQuandoValorNaoNumerico()
        {
            //a
            var linha = "BTC,valorNaoNumerico,2026-08-26,123456878612345";

            //a
            Assert.Throws<FormatException>(() => Cotacao.ConverterStringParaCotacao(linha));
        }
    }
}
