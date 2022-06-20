
namespace TalentsApi.Services.IServices
{
    public interface IImageService
    {
        string getFileName(string _fileName, string _uniqueIdx = "");
        string getPublicPath(string _directory, string _fileName);
        Task<string> saveImageAsync(Stream _stream, string _fileName, string _directory, string _uniqueIdx = "");
        Task<string> saveImageAsync(IFormFile _stream, string _fileName, string _directory, string _uniqueIdx = "");

    }
}