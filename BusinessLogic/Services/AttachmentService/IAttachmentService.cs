using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
        public string? Upload(IFormFile file , string folderName);
        bool Delete(string filePath);
    }
}
