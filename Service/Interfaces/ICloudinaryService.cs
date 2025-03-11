using Microsoft.AspNetCore.Http;

namespace Service.Interfaces;

public interface ICloudinaryService
{
    Task<string> UploadFile(IFormFile file);
}