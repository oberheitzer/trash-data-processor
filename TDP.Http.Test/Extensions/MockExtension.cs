using Moq;
using Moq.Protected;
using Moq.Language.Flow;

namespace TDP.Http.Test;

internal static class MockExtension
{
    internal static ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SetupSendAsync(this Mock<HttpMessageHandler> handlerMock)
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
