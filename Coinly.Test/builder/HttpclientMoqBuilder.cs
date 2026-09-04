using System.Net;
using Moq;
using Moq.Protected;

namespace Coinly.Test.builder;

public class HttpclientMoqBuilder
{
    
    public static Mock<HttpClient> GetMock(string respostaDaApiEmJson, HttpStatusCode statusCode, out Mock<HttpMessageHandler> handler)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(respostaDaApiEmJson)
            });

        var httpClientFake = new Mock<HttpClient>(handlerMock.Object);

        handler = handlerMock;
        return httpClientFake;
    }
}