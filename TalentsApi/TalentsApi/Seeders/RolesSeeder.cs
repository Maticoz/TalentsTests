using Microsoft.EntityFrameworkCore;
using TalentsApi.Entities;

namespace TalentsApi.Seeders
{
    public class RolesSeeder
    {
        private readonly AppDbContext _dbContext;

        public RolesSeeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (_dbContext.Database.IsRelational())
                {
                }
            }
        }
    }
}
