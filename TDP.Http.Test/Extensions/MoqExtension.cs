using Moq;
using Moq.Protected;

namespace TDP.Http.Test;

internal static class MoqExtension
{
    internal static Moq.Language.Flow.ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SetupSendAsync(this Mock<HttpMessageHandler> handlerMock)
    {
        return handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                methodOrPropertyName: "SendAsync", 
                ItExpr.Is<HttpRequestMessage>(request => request.RequestUri != null && !string.IsNullOrEmpty(request.RequestUri.ToString())), 
                ItExpr.IsAny<CancellationToken>()
            );
    }
}
