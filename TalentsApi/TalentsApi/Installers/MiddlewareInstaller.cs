using TalentsApi.Middleware;

namespace TalentsApi.Installers
{
    public class MiddlewareInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<ErrorHandlingMiddleware>();
        }
    }
}
