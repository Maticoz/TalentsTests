using Microsoft.Extensions.Localization;

namespace TalentsApi.Services.IServices
{
    public interface ILanguageService
    {
        LocalizedString GetKey(string key);
        string GetTranslated(string key);
    }
}