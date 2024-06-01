namespace GatewayService.Services;

public interface IHttpClientService
{
    Task<string> SendPayload(Object payload, string mediaType, string targeturl);
}