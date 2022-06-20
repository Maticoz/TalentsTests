using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using TalentsApi.Models;
using TalentsApi.Services;
using TalentsApi.Services.IServices;

namespace TalentsApi.Installers.Services
{
    public class LanguageInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILanguageService, LanguageService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("ShareResource", assemblyName.Name);
                    };

                });

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                        {
                new CultureInfo("pl-PL"),
                new CultureInfo("en-US"),
                        };
                    options.DefaultRequestCulture = new RequestCulture(culture: "pl-PL", uiCulture: "pl-PL");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                    {
                        var userLangs = context.Request.Headers["Accept-Language"].ToString();
                        var firstLang = userLangs.Split(',').FirstOrDefault();
                        var defaultLang = string.IsNullOrEmpty(firstLang) ? "pl-PL" : firstLang;
                        return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                    }));
                });
        }
    }
}
