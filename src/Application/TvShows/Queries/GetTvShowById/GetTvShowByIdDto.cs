using Application.Mappings;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.TvShows.Queries.GetTvShowById
{
    public class GetTvShowByIdDto : IMapFrom<TvShow>
    {
        public int Id { get; set; }
        public int TvMazeId { get; set; }
        public string Name { get; set; } = null!;
        public string Language { get; set; } = null!;
        public DateTime Premiered { get; set; }
        public ICollection<Genre> Genres { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public Rating Rating { get; set; } = null!;
    }
}