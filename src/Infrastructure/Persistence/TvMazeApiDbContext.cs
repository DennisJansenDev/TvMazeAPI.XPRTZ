using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class TvMazeApiDbContext : DbContext, ITvMazeApiDbContext
    {
        public TvMazeApiDbContext(DbContextOptions<TvMazeApiDbContext> options) : base(options)
        {
        }
        public DbSet<TvShow> TvMazeShows => Set<TvShow>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
