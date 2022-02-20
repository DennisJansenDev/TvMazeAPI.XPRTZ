using Domain.Entities;
using Domain.UnitTests.Builders;
using System;
using System.Linq;
using Xunit;

namespace Domain.UnitTests.Entitites
{
    public class TvShowTests
    {
        [Theory]
        [InlineData(4.20)]
        [InlineData(9.9)]
        public void ShouldUpdateRatingCorrectly(double expectedRating)
        {
            var tvShow = new TvShowBuilder().Build();

            tvShow.UpdateRating(expectedRating);

            Assert.Equal(expectedRating, tvShow.Rating.Average);
        }

        [Fact]
        public void ShouldThrowArgumentOutOfRangeExceptionWhenMoreThan3GenresAreAdded()
        {
            var tvShow = new TvShowBuilder().WithGenres(GenreType.Action, GenreType.Adventure, GenreType.Anime).Build();

            Assert.Throws<ArgumentOutOfRangeException>(() => tvShow.AddGenre(GenreType.Drama));
        }

        [Theory]
        [InlineData(GenreType.Comedy, null, 2)]
        [InlineData(GenreType.Comedy, GenreType.Espionage, 3)]
        public void ShouldAddCorrectGenres(GenreType genreType, GenreType? genreType2, int amountGenres)
        {
            var tvShow = new TvShowBuilder().Build();

            tvShow.AddGenre(genreType);

            if (genreType2.HasValue)
                tvShow.AddGenre(genreType2.Value);

            Assert.Equal(amountGenres, tvShow.Genres.Count());
        }

        [Theory]
        [InlineData("NameUpdated", "en", "BlablaSummary", 1, "UpdateNameOnly", "en", "BlablaSummary", 1)]
        [InlineData("UpdateLanguageOnly", "jp", "BlablaSummary", 1, "UpdateLanguageOnly", "en", "BlablaSummary", 1)]
        [InlineData("UpdateSummaryOnly", "en", "BlaBlaUpdatedSummary", 1, "UpdateSummaryOnly", "en", "BlablaSummary", 1)]
        [InlineData("UpdateTvMazeIdOnly", "en", "BlablaSummary", 8, "UpdateTvMazeIdOnly", "en", "BlablaSummary", 1)]
        [InlineData("NameUpdated", "jp", "BlaBlaUpdatedSummary", 420, "UpdateEverything", "en", "BlablaSummary", 1)]
        public void ShouldUpdateTvShowName(string? updatedName, 
            string? updatedLanguage, 
            string? updatedSummary, 
            int? updatedTvMazeId, 
            string name, 
            string language, 
            string summary, 
            int tvMazeId)
        {
            var tvShow = new TvShowBuilder()
                .WithName(name)
                .WithSummary(summary)
                .WithLanguage(language)
                .WithTvMazeId(tvMazeId)
                .Build();

            tvShow.UpdateTvShow(updatedName, updatedLanguage, updatedSummary, updatedTvMazeId);

            Assert.Equal(updatedName, tvShow.Name);
            Assert.Equal(updatedLanguage, tvShow.Language);
            Assert.Equal(updatedSummary, tvShow.Summary);
            Assert.Equal(updatedTvMazeId, tvShow.TvMazeId);
        }
    }
}
