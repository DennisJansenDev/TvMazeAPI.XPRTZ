using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.UnitTests.Builders
{
    public class TvShowBuilder
    {
        private string _name;
        private string _language;
        private string _summary;
        private int? _tvMazeId;
        private DateTime _premiered;
        private List<Genre> _genres;
        private Rating _rating;

        public TvShowBuilder()
        {
            _name = "Test TvShow";
            _language = "en";
            _summary = "SGV5IGlrIGhvb3AgZGF0IGp1bGxpZSBuZXQgem92ZWVsIHBsZXppZXIgaGViYmVuIGFhbiBoZXQgbGV6ZW4gdmFuIGRlIGNvZGUgYWxzIGRhdCBpayBoZWIgZ2VoYWQgdGlqZGVucyBoZXQgc2NocmlqdmVuISBFbiBob29wIGRhdCBpayBqdWxsaWUgbmlldXdlIGNvbGxlZ2Ega2FuIHdvcmRlbiBiaW5uZW5rb3J0IDop";
            _genres = new List<Genre> { new Genre { GenreType = GenreType.Action } };
            _premiered = DateTime.Now;
            _rating = Rating.FromDouble(2.0);
        }

        public TvShowBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public TvShowBuilder WithLanguage(string language)
        {
            _language = language;
            return this;
        }
        public TvShowBuilder WithSummary(string summary)
        {
            _summary = summary;
            return this;
        }
        public TvShowBuilder WithPremiered(DateTime premiered)
        {
            _premiered = premiered;
            return this;
        }
        public TvShowBuilder WithGenres(GenreType genre1, GenreType? genre2, GenreType? genre3)
        {
            var genre = new Genre
            {
                GenreType = genre1,
            };

            _genres.Add(genre);

            if (genre2 != null)
                _genres.Add(new Genre{ GenreType = genre2.Value });

            if (genre3 != null)
                _genres.Add(new Genre { GenreType = genre3.Value });

            return this;
        }

        public TvShowBuilder WithRating(double rating)
        {
            _rating = Rating.FromDouble(rating);
            return this;
        }

        public TvShowBuilder WithTvMazeId(int tvMazeId) 
        {
            _tvMazeId = tvMazeId;
            return this;
        }

        public TvShow Build()
        {
            return new TvShow(_name, _language, _premiered, _genres, _summary, _rating, _tvMazeId.HasValue ? _tvMazeId.Value : null);
        }
    }
}
