using System.Net;
using Coinly.Modelos;
using Coinly.Services;
using Coinly.Test.builder;
using Moq;
using Moq.Protected;

namespace Coinly.Test;

[Collection("Console")]
public class CotacaoServiceProcessarConsulta
{
    private const string JsonListaMoedasValida = "{\n  \"USD\": \"Dólar Americano\",\n  \"EUR\": \"Euro\",\n  \"BRL\": \"Real Brasileiro\"\n}";
    private const string JsonCotacaoValida = "{\n  \"USDBRL\": {\n    \"code\": \"USD\",\n    \"bid\": \"5.20\",\n    \"create_date\": \"2026-09-04 10:00:00\",\n    \"timestamp\": \"1234567890\"\n  }\n}";

    // 2 regras no mesmo handler: URL com "available" -> resposta da lista; URL com "last" -> resposta da cotação.
    private static Mock<HttpMessageHandler> CriarHandlerComRespostasPorUrl(HttpResponseMessage respostaListaMoedas, HttpResponseMessage respostaCotacao)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("available")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(respostaListaMoedas);

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("last")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(respostaCotacao);

        return handlerMock;
    }

    [Fact]
    public async Task QuandoMoedaNaoExisteNaListaExibeMensagemENaoChamaApiCotar()
    {
        //a
        var escritorArquivo = EscritorArquivoMoqBuilder.GetMock();
        var httpClient = HttpclientMoqBuilder.GetMock(JsonListaMoedasValida, HttpStatusCode.OK, out var handlerMock);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var moedaQueNaoExisteNaLista = "AAA";

        //a
        await CotacaoService.ProcessarConsulta(moedaQueNaoExisteNaLista, httpClient.Object, escritorArquivo.Object);

        //a
        var mensagemConsole = stringWriter.ToString();
        Assert.Equal($"\n{moedaQueNaoExisteNaLista} não disponível\n\n", mensagemConsole);
        handlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
            //Times.Once() verifica que teve 1 chamada (CarregarMoedas()), se chamar 2 quer dizer que ApiCotar() rodou
        );
        escritorArquivo.Verify(x => x.EscreverNoArquivoCSV(It.IsAny<Cotacao>()), Times.Never());
    }

    [Fact]
    public async Task QuandoCarregarMoedaRetornaNullExibeMensagem()
    {
        //a
        var escritorArquivo = EscritorArquivoMoqBuilder.GetMock();
        var httpClient = HttpclientMoqBuilder.GetMock(
            "{ JSON MAL FORMADO PARA GERAR NULL",
            HttpStatusCode.OK, out var handlerMock);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //a
        await CotacaoService.ProcessarConsulta("USD", httpClient.Object, escritorArquivo.Object);

        //a
        var mensagemConsole = stringWriter.ToString();
        Assert.Equal("Erro de parse/formato de JSON.\n\nUSD não disponível\n\n", mensagemConsole);
    }

    [Fact]
    public async Task QuandoApiCotarRetornaNullNaoEscreveNoArquivo()
    {
        //a
        var escritorArquivo = EscritorArquivoMoqBuilder.GetMock();
        var respostaListaMoedas = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonListaMoedasValida) };
        var respostaCotacao = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{ JSON MAL FORMADO") };
        var handlerMock = CriarHandlerComRespostasPorUrl(respostaListaMoedas, respostaCotacao);
        var httpClient = new HttpClient(handlerMock.Object);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //a
        await CotacaoService.ProcessarConsulta("USD", httpClient, escritorArquivo.Object);

        //a
        escritorArquivo.Verify(x => x.EscreverNoArquivoCSV(It.IsAny<Cotacao>()), Times.Never());
    }

    [Fact]
    public async Task QuandoSucessoCompletoEscreveNoArquivoUmaVez()
    {
        //a
        var escritorArquivo = EscritorArquivoMoqBuilder.GetMock();
        var respostaListaMoedas = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonListaMoedasValida) };
        var respostaCotacao = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonCotacaoValida) };
        var handlerMock = CriarHandlerComRespostasPorUrl(respostaListaMoedas, respostaCotacao);
        var httpClient = new HttpClient(handlerMock.Object);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //a
        await CotacaoService.ProcessarConsulta("USD", httpClient, escritorArquivo.Object);

        //a
        var mensagemConsole = stringWriter.ToString();
        var cabecalho = $"{"Moeda",-10}{"Valor",-15}{"Data",-15}{Environment.NewLine}";
        var linhaCotacao = $"{"USD",-10}{5.20,-15}{"2026-09-04 10:00:00",-15}{Environment.NewLine}";
        Assert.Equal(cabecalho + linhaCotacao + "--------------------------------------------" + Environment.NewLine, mensagemConsole);
        escritorArquivo.Verify(x => x.EscreverNoArquivoCSV(It.Is<Cotacao>(c => c.Sigla == "USD" && c.Valor == 5.20)), Times.Once());
    }

    [Fact]
    public async Task QuandoEscritorArquivoLancaExcecaoCaiNoCatchGenerico()
    {
        //a
        var escritorArquivo = EscritorArquivoMoqBuilder.GetMock();
        escritorArquivo.Setup(x => x.EscreverNoArquivoCSV(It.IsAny<Cotacao>()))
            .ThrowsAsync(new IOException("disco cheio"));

        var respostaListaMoedas = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonListaMoedasValida) };
        var respostaCotacao = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonCotacaoValida) };
        var handlerMock = CriarHandlerComRespostasPorUrl(respostaListaMoedas, respostaCotacao);
        var httpClient = new HttpClient(handlerMock.Object);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //a
        await CotacaoService.ProcessarConsulta("USD", httpClient, escritorArquivo.Object);

        //a
        var mensagemConsole = stringWriter.ToString();
        Assert.EndsWith($"Houve um erro: disco cheio{Environment.NewLine}", mensagemConsole);
    }
}
