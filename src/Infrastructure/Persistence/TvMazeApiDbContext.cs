using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class TvMazeApiDbContext : DbContext, ITvMazeApiDbContext
    {
        public DbSet<TvMazeShow> TvMazeShows => Set<TvMazeShow>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
