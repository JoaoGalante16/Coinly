using System.Net;
using Coinly.Clients;
using Coinly.Test.builder;
using Moq;
using Moq.Protected;

namespace Coinly.Test;

[Collection("Console")]
public class ApiListaMoedasClientCarregarMoedas
{
    [Fact]
    public async Task QuandoJsonValidoRetornaListaDeMoedas()
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock("{\n  \"USD\": \"Dólar Americano\",\n  \"EUR\": \"Euro\",\n  \"BRL\": \"Real Brasileiro\"\n}", HttpStatusCode.OK, out var handlerMock);
        
        //a
        var resultado = await ApiListaMoedasClient.CarregarMoedas(httpClient.Object);

        //a
        Assert.Contains("USD", resultado.Keys);
        Assert.Contains("Dólar Americano", resultado.Values);
        Assert.Contains("EUR", resultado.Keys);
        Assert.Contains("Euro", resultado.Values);
        Assert.Contains("BRL", resultado.Keys);
        Assert.Contains("Real Brasileiro", resultado.Values);
    }
    
    [Theory]
    [InlineData(HttpStatusCode.TooManyRequests, "Limite de requisições atingido. Aguarde antes de tentar novamente.\n", "{\n  \"USD\": \"Dólar Americano\",\n  \"EUR\": \"Euro\",\n  \"BRL\": \"Real Brasileiro\"\n}")]
    [InlineData(HttpStatusCode.NotFound, "A URL da API ou o recurso solicitado não foi encontrado.\n", "{\n  \"USD\": \"Dólar Americano\",\n  \"EUR\": \"Euro\",\n  \"BRL\": \"Real Brasileiro\"\n}")]
    [InlineData(HttpStatusCode.OK, "Erro de parse/formato de JSON.\n", "{ \"USD\": \"Dólar Americano\", \"EUR\": \"Eu")]
    public async Task QuandoErroRetornaNullEMensagemDeErro(HttpStatusCode statusCode, string mensagemDeErro, string jsonRetorno)
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock(jsonRetorno, statusCode, out var handlerMock);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        //a
        var resultado = await ApiListaMoedasClient.CarregarMoedas(httpClient.Object);
        
        //a
        string mensagemNoConsole = stringWriter.ToString();
        Assert.Null(resultado);
        Assert.Equal(mensagemDeErro, mensagemNoConsole);
    }
    
    [Fact]
    public async Task QuandoJsonVazioRetornaListaDeMoedasVazia()
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock("{}", HttpStatusCode.OK, out var handlerMock);
        
        //a
        var resultado = await ApiListaMoedasClient.CarregarMoedas(httpClient.Object);
        
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }
    
    [Fact]
    public async Task VerificaQualUrlChamada()
    {
        //a
        var httpClient = HttpclientMoqBuilder.GetMock("{\n  \"USD\": \"Dólar Americano\",\n  \"EUR\": \"Euro\",\n  \"BRL\": \"Real Brasileiro\"\n}", HttpStatusCode.OK, out var handlerMock);
        
        //a
        var resultado = await ApiListaMoedasClient.CarregarMoedas(httpClient.Object);
        
        //a
        handlerMock.Protected().Verify("SendAsync",Times.Once(),ItExpr.IsAny<HttpRequestMessage>(),ItExpr.IsAny<CancellationToken>());
        
    }
    
}

