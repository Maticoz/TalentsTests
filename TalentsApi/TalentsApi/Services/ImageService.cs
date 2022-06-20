using System.IO;
using TalentsApi.Entities;
using TalentsApi.Services.IServices;

namespace TalentsApi.Services
{
    public class ImageService : IImageService
    {
        private readonly string rootPath = Directory.GetCurrentDirectory();
        public const string publicPath = $"/uploads/";
        public const string avatars = "avatars";
        public const string events = "events";

        public ImageService()
        {
            string avatarsPath = $"{rootPath}/wwwroot/{ImageService.events}";
            string eventsPath = $"{rootPath}/wwwroot/{ImageService.events}";

            if (!Directory.Exists(avatarsPath))
            {
                Directory.CreateDirectory(avatarsPath);
            }
            if (!Directory.Exists(eventsPath))
            {
                Directory.CreateDirectory(eventsPath);
            }
        }

        public string getPublicPath(string _directory, string _fileName)
        {
            return $"{ImageService.publicPath}{_directory}/{_fileName}";
        }

        public string getFileName(string _fileName, string _uniqueIdx = "")
        {
            return $"{_uniqueIdx}-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}-{_fileName}";
        }

        public async Task<string> saveImageAsync(Stream _stream, string _fileName, string _directory, string _uniqueIdx = "")
        {
            var publicPath = this.getPublicPath(_directory, this.getFileName(_fileName,_uniqueIdx));
            var fullPath = $"{rootPath}/wwwroot{publicPath}";

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await _stream.CopyToAsync(stream);
            }
            return publicPath;
        }
        public async Task<string> saveImageAsync(IFormFile _stream, string _fileName, string _directory, string _uniqueIdx = "")
        {
            var publicPath = this.getPublicPath(_directory, this.getFileName(_fileName, _uniqueIdx));
            var fullPath = $"{rootPath}/wwwroot{publicPath}";

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await _stream.CopyToAsync(stream);
            }
            return publicPath;
        }
    }
}
