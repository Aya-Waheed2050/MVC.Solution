using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> _allowedExtensions = [ ".jpg", ".png", ".pdf" ];
        const int _maxFileSize = 2_097_152;
       
        public string? Upload(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_allowedExtensions.Contains(extension)) return null;
            if (file.Length == 0 || file.Length > _maxFileSize) return null;

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot\\Files" , folderName);
            var FileName = $"{Guid.NewGuid()}_{file.FileName}";

            var FilePath = Path.Combine(FolderPath, FileName);

            using FileStream fs = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(fs);

            return FileName;
        }


        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }

    }
}
