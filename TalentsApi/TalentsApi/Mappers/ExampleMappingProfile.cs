using AutoMapper;
using TalentsApi.Entities;
using TalentsApi.Services.IServices;

namespace TalentsApi.Mappers
{
    public class ExampleMappingProfile : Profile
    {
        public ExampleMappingProfile(ILanguageService languageService)
        {
        }
    }
}
