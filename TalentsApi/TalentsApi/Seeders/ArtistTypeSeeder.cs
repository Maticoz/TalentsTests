using TalentsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace TalentsApi.Seeders
{
    public class ArtistTypeSeeder
    {
        private readonly AppDbContext _dbContext;

        public ArtistTypeSeeder(AppDbContext dbContext)
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
