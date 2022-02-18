using Domain.Common;

namespace Domain.Entities
{
    public class TvShow
    {
        public int Id { get; private set; }
        public int TvMazeId { get; private set; }
        public string Name { get; private set; }
        public string Language { get; private set; }
        public DateTime Premiered { get; private set; }
        public ICollection<Genre> Genres { get; private set; }
        public string Summary { get; private set; }
        public Rating Rating { get; private set; } = Rating.FromDouble(1.0);

        //Private parameterless constructor for EFCore
        private TvShow()
        {
        }

        public TvShow(string name, string language, DateTime premiered, List<Genre> genres, string summary, Rating rating, int? tvMazeId)
        {
            Name = name;
            Language = language;
            Premiered = premiered;
            Genres = genres;
            Summary = summary;
            Rating = rating;

            if (tvMazeId.HasValue)
                TvMazeId = tvMazeId.Value;
        }
    }
    public class Genre
    {
        public int Id { get; set; }
        public GenreType GenreType { get; set; }

        public override string ToString() => GenreType.ToString();
    }

    public enum GenreType { Action, Adventure, Anime, Comedy, Crime, Drama, Espionage, Family, Fantasy, History, Horror, Legal, Medical, Music, Mystery, Romance, ScienceFiction, Sports, Supernatural, Thriller, War, Western };

    public class Rating : ValueObject
    {
        public double Average { get; private set; }

        private Rating(double average)
        {
            if (average < 1.0 || average > 10.0)
                throw new ArgumentOutOfRangeException(nameof(average), "Supply a double between 1.0 and 10.0");

            Average = average;
        }

        public static Rating FromDouble(double average) => new Rating(average);

        public override string ToString() => "Rating: " + Average;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Average;
        }
    }
}