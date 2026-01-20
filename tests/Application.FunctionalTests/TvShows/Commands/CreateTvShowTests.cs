using Application.TvShows.Commands.CreateTvShow;
using Domain.Entities;

namespace Application.FunctionalTests.TvShows.Commands;

public class CreateTvShowTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateTvShow()
    {
        // Arrange
        var command = new CreateTvShowCommand
        {
            Name = "Breaking Bad",
            Language = "English",
            Premiered = new DateTime(2008, 1, 20),
            Genres = new List<Genre>
            {
                new Genre { GenreType = GenreType.Drama },
                new Genre { GenreType = GenreType.Crime },
                new Genre { GenreType = GenreType.Thriller }
            },
            Summary = "A high school chemistry teacher turned methamphetamine producer.",
            AverageRating = 9.5,
            TvMazeId = 169
        };

        // Act
        var tvShowId = await SendAsync(command);

        // Assert
        var tvShow = await FindAsync<TvShow>(tvShowId);

        tvShow.Should().NotBeNull();
        tvShow!.Name.Should().Be("Breaking Bad");
        tvShow.Language.Should().Be("English");
        tvShow.Premiered.Should().Be(new DateTime(2008, 1, 20));
        tvShow.Summary.Should().Be("A high school chemistry teacher turned methamphetamine producer.");
        tvShow.Rating.Average.Should().Be(9.5);
        tvShow.TvMazeId.Should().Be(169);
        tvShow.Genres.Should().HaveCount(3);
    }

    [Test]
    public async Task ShouldRequireName()
    {
        // Arrange
        var command = new CreateTvShowCommand
        {
            Name = string.Empty,
            Language = "English",
            Premiered = DateTime.Now,
            Genres = new List<Genre>(),
            Summary = "Test summary",
            AverageRating = 5.0
        };

        // Act & Assert
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<Exception>();
    }
}
