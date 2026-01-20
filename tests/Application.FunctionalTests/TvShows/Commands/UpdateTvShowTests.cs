using Application.Exceptions;
using Application.TvShows.Commands.CreateTvShow;
using Application.TvShows.Commands.UpdateTvShow;
using Domain.Entities;

namespace Application.FunctionalTests.TvShows.Commands;

public class UpdateTvShowTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateTvShow()
    {
        // Arrange - Create a TV show first
        var createCommand = new CreateTvShowCommand
        {
            Name = "Test Show",
            Language = "English",
            Premiered = new DateTime(2020, 1, 1),
            Genres = new List<Genre> { new Genre { GenreType = GenreType.Drama } },
            Summary = "Original summary",
            AverageRating = 7.0,
            TvMazeId = 123
        };

        var tvShowId = await SendAsync(createCommand);

        var updateCommand = new UpdateTvShowCommand
        {
            Id = tvShowId,
            Name = "Updated Test Show",
            Language = "Spanish",
            Summary = "Updated summary",
            TvMazeId = 456
        };

        // Act
        await SendAsync(updateCommand);

        // Assert
        var tvShow = await FindAsync<TvShow>(tvShowId);

        tvShow.Should().NotBeNull();
        tvShow!.Name.Should().Be("Updated Test Show");
        tvShow.Language.Should().Be("Spanish");
        tvShow.Summary.Should().Be("Updated summary");
        tvShow.TvMazeId.Should().Be(456);
    }

    [Test]
    public async Task ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new UpdateTvShowCommand
        {
            Id = 99999,
            Name = "Non-existent Show"
        };

        // Act & Assert
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }
}
