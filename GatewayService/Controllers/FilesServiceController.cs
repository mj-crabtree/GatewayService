using GatewayService.Entities;
using GatewayService.Services;
using Microsoft.AspNetCore.Mvc;

namespace GatewayService.Controllers;

[ApiController]
[Route("/api/files")]
public class FilesServiceController : ControllerBase
{
    private const string FilesUrl = "http://files-service:8080/api/files";
    private readonly IHttpClientService _httpClient;
    public FilesServiceController(IHttpClientService httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<string> ForwardFileGet(Guid id)
    {
        var data = new
        {
            FileId = id
        };
        return await _httpClient.SendPayload(data, "application/json", FilesUrl);
    }

    [HttpPost]
    public async Task<string> ForwardFileUpload(FileUploadPayloadDto payloadDto)
    {
        var data = new
        {
            File = payloadDto.File,
            ClassificationTier = payloadDto.ClassificationTier
        };
        return await _httpClient.SendPayload(data, "multipart/form-data", FilesUrl);
    }

    public class FileUploadPayloadDto
    {
        public IFormFile File { get; set; }
        public ClassificationTier ClassificationTier { get; set; }
    }

    public class FileServiceUploadResponseDto
    {
        public string Path { get; set; }
        public string ClassificationTier { get; set; }
        public string FileType { get; set; }
    }
}