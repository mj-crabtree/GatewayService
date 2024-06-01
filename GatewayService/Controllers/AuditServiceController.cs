using GatewayService.Services;
using Microsoft.AspNetCore.Mvc;

namespace GatewayService.Controllers;

[ApiController]
[Route("/api/auditEvents")]
public class AuditServiceController : ControllerBase
{
    private const string Auditurl = "audit-service:8080/api/events";
    private readonly IHttpClientService _httpClientService;

    public AuditServiceController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService ?? throw new ArgumentNullException(nameof(httpClientService));
    }

    [HttpGet]
    public async Task<string> ForwardGetEvent(Guid id)
    {
        var data = new
        {
            id = id
        };
        return await _httpClientService.SendPayload(data, "application/json", Auditurl);
    }
    
    public async Task<string> ForwardGetEvent(AuditQueryParameters parameters)
    {
        var data = new
        {
            AuditEventsResourceParameters = parameters
        };
        return await _httpClientService.SendPayload(data, "application/json", Auditurl);
    }

    public class AuditQueryParameters
    {
        public string? FileId { get; set; }
        public string? UserId { get; set; }
        public string? SearchQuery { get; set; }
    }
}