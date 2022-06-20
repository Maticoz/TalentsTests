namespace TalentsApi.Installers
{
    public class CorsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", policyBuilder =>
                    policyBuilder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(configuration["AllowedOrigins"])
                    );
            });
        }
    }
}
