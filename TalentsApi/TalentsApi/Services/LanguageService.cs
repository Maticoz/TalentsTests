using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using TalentsApi.Exceptions;
using TalentsApi.Models;
using TalentsApi.Services.IServices;

namespace TalentsApi.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IStringLocalizer _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguageService(IStringLocalizerFactory factory, IHttpContextAccessor httpContextAccessor)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
            _httpContextAccessor = httpContextAccessor;

        }
        public LocalizedString GetKey(string key)
        {
            return _localizer[key];
        }
        public string GetTranslated(string key)
        {
            var translated = _localizer[key];

            if (translated == null)
            {
                throw new NotFoundException($"Not found translated label key - {key}");
            }

            return translated.Value;
        }
    }
}
