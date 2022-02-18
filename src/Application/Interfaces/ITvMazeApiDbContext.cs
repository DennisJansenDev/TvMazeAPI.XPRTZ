using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ITvMazeApiDbContext
    {
        DbSet<TvShow> TvMazeShows { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}