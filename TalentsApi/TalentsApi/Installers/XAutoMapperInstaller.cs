using AutoMapper;
using Microsoft.Extensions.Localization;
using System.Reflection;
using TalentsApi.Mappers;
using TalentsApi.Services;
using TalentsApi.Services.IServices;

namespace TalentsApi.Installers
{
    public class XAutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ExampleMappingProfile(provider.GetService<ILanguageService>()));
            }).CreateMapper());
        }
    }
}
