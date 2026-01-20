using Application.TvShows.Commands.CreateTvShow;
using Application.TvShows.Queries.GetTvShows;
using Domain.Entities;

namespace Application.FunctionalTests.TvShows.Queries;

public class GetTvShowsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnAllTvShows()
    {
        // Arrange - Create some TV shows
        var show1 = new CreateTvShowCommand
        {
            Name = "Show 1",
            Language = "English",
            Premiered = new DateTime(2020, 1, 1),
            Genres = new List<Genre> { new Genre { GenreType = GenreType.Drama } },
            Summary = "Summary 1",
            AverageRating = 7.0,
            TvMazeId = 1
        };

        var show2 = new CreateTvShowCommand
        {
            Name = "Show 2",
            Language = "Spanish",
            Premiered = new DateTime(2021, 6, 15),
            Genres = new List<Genre> { new Genre { GenreType = GenreType.Comedy } },
            Summary = "Summary 2",
            AverageRating = 8.0,
            TvMazeId = 2
        };

        await SendAsync(show1);
        await SendAsync(show2);

        var query = new GetTvShowsQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Show 1"); // Ordered by Premiered date
        result.Last().Name.Should().Be("Show 2");
    }

    [Test]
    public async Task ShouldReturnEmptyListWhenNoTvShows()
    {
        // Arrange
        var query = new GetTvShowsQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Should().BeEmpty();
    }
}
