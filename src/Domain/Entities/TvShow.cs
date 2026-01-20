using Domain.ValueObjects;

namespace Domain.Entities
{
    public class TvShow
    {
        public int Id { get; private set; }
        public int TvMazeId { get; private set; }
        public string Name { get; private set; } = null!;
        public string Language { get; private set; } = null!;
        public DateTime Premiered { get; private set; }
        public string Summary { get; private set; } = null!;
        public Rating Rating { get; private set; } = Rating.FromDouble(1.0);

        private List<Genre> _genres = new List<Genre>();

        public IEnumerable<Genre> Genres => _genres.AsReadOnly();

        //Private parameterless constructor for EFCore
        public TvShow()
        {
        }

        public TvShow(string name, string language, DateTime premiered, List<Genre> genres, string summary, Rating rating, int? tvMazeId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Language = language ?? throw new ArgumentNullException(nameof(language));
            Summary = summary ?? throw new ArgumentNullException(nameof(summary));
            Rating = rating ?? throw new ArgumentNullException(nameof(rating));
            _genres = genres ?? throw new ArgumentNullException(nameof(genres));

            Premiered = premiered;

            if (tvMazeId.HasValue)
                TvMazeId = tvMazeId.Value;
        }

        public void UpdateTvShow(string? name, string? language, string? summary, int? tvMazeId)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;
            if (!string.IsNullOrEmpty(language))
                Language = language;
            if (!string.IsNullOrEmpty(summary))
                Summary = summary;
            if (tvMazeId.HasValue)
                TvMazeId = tvMazeId.Value;
        }

        public void AddGenre(GenreType genreType)
        {
            if (_genres.Count >= 3)
                throw new ArgumentOutOfRangeException("A TvShow can only hold 3 types of genres.");

            _genres.Add(new Genre { GenreType = genreType });
        }

        public void UpdateRating(double newRating)
        {
            Rating = Rating.FromDouble(newRating);
        }
    }
}