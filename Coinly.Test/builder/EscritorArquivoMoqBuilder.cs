using Coinly.Modelos;
using Coinly.Utilities;
using Moq;

namespace Coinly.Test.builder;

public class EscritorArquivoMoqBuilder
{
    public static Mock<EscritorArquivo> GetMock()
    {
        var mock = new Mock<EscritorArquivo>(MockBehavior.Default);

        mock.Setup(x => x.EscreverNoArquivoCSV(It.IsAny<Cotacao>()))
            .Returns(Task.CompletedTask);
        
        return mock;
    }
}