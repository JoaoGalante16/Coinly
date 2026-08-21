using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Test
{
    [Collection("Console")]
    public class CotacaoMostrarCotacao
    {
        [Fact]
        public void ValidaMostrarCotacaoQaundoDadosCertos()
        {
            //a
            var cotacao = new Cotacao() { Sigla = "BTC", Valor = 5500.00, DataHora = "2026-07-16"};
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var respostaEsperada = $"{"BTC",-10}{5500.00,-15}{"2026-07-16",-15}";

            //a
            cotacao.MostrarCotacao();
            string resposta = stringWriter.ToString().TrimEnd('\r', '\n');

            //a
            Assert.Equal(respostaEsperada, resposta);
        }

        [Theory]
        [InlineData("BTC123456789", 5500.00 , "2026-07-16")]
        [InlineData("BTC", 5500.00, "2026-07-16123456789123456789")]
        [InlineData("BTC", 5500550005550005550005.00, "2026-07-16")]
        public void ValidaMostarCotacaoQaundoCampoMaiorQuePadding(string sigla, double valor, string data)
        {
            var cotacao = new Cotacao() { Sigla = sigla, Valor = valor, DataHora = data };
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var respostaEsperada = $"{sigla,-10}{valor,-15}{data,-15}";

            //a
            cotacao.MostrarCotacao();
            string resposta = stringWriter.ToString().TrimEnd('\r', '\n');

            //a
            Assert.Equal(respostaEsperada, resposta);
        }

    }
}
