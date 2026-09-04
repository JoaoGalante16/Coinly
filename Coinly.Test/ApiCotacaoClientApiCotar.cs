using System.Net;
using Coinly.Clients;
using Moq;
using Moq.Protected;
using Xunit.Sdk;

namespace Coinly.Test.builder;

public class ApiCotacaoClientApiCotar
{
    [Fact]
    public async Task QuandoJsonValidoRetornaCotacao()
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock("{\n  \"USDBRL\": {\n    \"code\": \"USD\",\n    \"bid\": \"5.20\",\n    \"create_date\": \"2026-09-04 10:00:00\",\n    \"timestamp\": \"1234567890\"\n  }\n}", HttpStatusCode.OK, out var handlerMock);
        
        //a
        var resultado = await ApiCotacaoClient.ApiCotar("usd",httpClient.Object);

        //a
        Assert.Equal("USD", resultado.Sigla);
        Assert.Equal(5.20, resultado.Valor);
        Assert.Equal("2026-09-04 10:00:00", resultado.DataHora);
    }

    [Theory]
    [InlineData(HttpStatusCode.TooManyRequests, "Limite de requisições atingido. Aguarde antes de tentar novamente.\n", "{\n  \"USDBRL\": {\n    \"code\": \"USD\",\n    \"bid\": \"5.20\",\n    \"create_date\": \"2026-09-04 10:00:00\",\n    \"timestamp\": \"1234567890\"\n  }\n}")]
    [InlineData(HttpStatusCode.NotFound, "A URL da API ou o recurso solicitado não foi encontrado.\n", "{\n  \"USDBRL\": {\n    \"code\": \"USD\",\n    \"bid\": \"5.20\",\n    \"create_date\": \"2026-09-04 10:00:00\",\n    \"timestamp\": \"1234567890\"\n  }\n}")]
    [InlineData(HttpStatusCode.OK, "Erro de parse/formato de JSON.\n", "{ \"USDBRL\": { \"code\": \"USD\", \"bid\": ")]
    public async Task QuandoErroRetornaNullEMensagemDeErro(HttpStatusCode statusCode, string mensagemDeErro, string jsonRetorno)
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock(jsonRetorno, statusCode, out var handlerMock);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        //a
        var resultado = await ApiCotacaoClient.ApiCotar("usd", httpClient.Object);
        
        //a
        string mensagemNoConsole = stringWriter.ToString();
        Assert.Null(resultado);
        Assert.Equal(mensagemDeErro, mensagemNoConsole);
    }

    [Fact]
    public async Task QuandoJsonVazioEstouraInvalidOperationException()
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock("{}", HttpStatusCode.OK, out var handlerMock);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        
        //a
        var resultado = await ApiCotacaoClient.ApiCotar("usd", httpClient.Object);

        //a
        string mensagemDoConsole = stringWriter.ToString();
        Assert.Null(resultado);
        Assert.StartsWith("Erro: ", mensagemDoConsole);
    }

    [Fact]
    public async Task VerificaQualAUrlChamada()
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock("{\n  \"USDBRL\": {\n    \"code\": \"USD\",\n    \"bid\": \"5.20\",\n    \"create_date\": \"2026-09-04 10:00:00\",\n    \"timestamp\": \"1234567890\"\n  }\n}", HttpStatusCode.OK, out var handlerMock);
        
        //a
        var resultado = await ApiCotacaoClient.ApiCotar("usd", httpClient.Object);
        
        //a
        handlerMock.Protected().Verify("SendAsync",Times.Once(),ItExpr.IsAny<HttpRequestMessage>(),ItExpr.IsAny<CancellationToken>());
        
    }
}