using TalentsApi.Seeders;

namespace TalentsApi.Installers
{
    public class SeederInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
        }
        public static void Seed(WebApplication app)
        {
            var scope = app.Services.CreateScope();
        }
    }
}
