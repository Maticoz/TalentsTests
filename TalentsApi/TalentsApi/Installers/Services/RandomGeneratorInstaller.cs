using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentsApi.Services.IServices;
using TalentsApi.Services;

namespace TalentsApi.Installers.Services
{
    public class RandomGeneratorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRandomGeneratorService, RandomGeneratorService>();

        }
    }
}
