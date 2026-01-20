using Application.Exceptions;
using Application.TvShows.Commands.CreateTvShow;
using Application.TvShows.Commands.DeleteTvShow;
using Domain.Entities;

namespace Application.FunctionalTests.TvShows.Commands;

public class DeleteTvShowTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDeleteTvShow()
    {
        // Arrange - Create a TV show first
        var createCommand = new CreateTvShowCommand
        {
            Name = "Show to Delete",
            Language = "English",
            Premiered = new DateTime(2020, 1, 1),
            Genres = new List<Genre> { new Genre { GenreType = GenreType.Drama } },
            Summary = "This show will be deleted",
            AverageRating = 5.0,
            TvMazeId = 789
        };

        var tvShowId = await SendAsync(createCommand);

        var deleteCommand = new DeleteTvShowCommand { Id = tvShowId };

        // Act
        await SendAsync(deleteCommand);

        // Assert
        var tvShow = await FindAsync<TvShow>(tvShowId);
        tvShow.Should().BeNull();
    }

    [Test]
    public async Task ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new DeleteTvShowCommand { Id = 99999 };

        // Act & Assert
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }
}
