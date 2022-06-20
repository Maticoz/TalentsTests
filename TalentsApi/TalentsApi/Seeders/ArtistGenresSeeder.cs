using TalentsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace TalentsApi.Seeders
{
    public class ArtistGenresSeeder
    {
        private readonly AppDbContext _dbContext;

        public ArtistGenresSeeder(AppDbContext dbContext)
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
