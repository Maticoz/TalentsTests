using Microsoft.EntityFrameworkCore;
using TalentsApi.Entities;

namespace TalentsApi.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["DBHOST"] ?? "localhost";
            var port = configuration["DBPORT"] ?? "3306";
            var password = configuration["MYSQL_PASSWORD"] ?? configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = configuration["MYSQL_USER"] ?? configuration.GetConnectionString("MYSQL_USER");
            var usersDataBase = configuration["MYSQL_DATABASE"] ?? configuration.GetConnectionString("MYSQL_DATABASE");
            string connString;

            if (password != null && userid != null)
            {
                connString = $"server={host}; userid={userid};pwd={password};port={port};database={usersDataBase}";

            }
            else
            {
                connString = configuration.GetConnectionString("DbConnection");
                if(String.IsNullOrWhiteSpace(connString))
                {
                    connString = $"server=localhost; userid=talentsTests;pwd=talentsTests;port=3306;database=talentsTests";
                }
            }

            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            if(isDevelopment)
            {
                Console.WriteLine(connString);
            }
            services.AddDbContext<AppDbContext>
            (options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));
        }

    }
}
