using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ITvMazeApiDbContext
    {
        DbSet<TvMazeShow> TvMazeShows { get; }
    }
}