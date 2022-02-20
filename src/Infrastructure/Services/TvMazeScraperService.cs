using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.Dto;
using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public class TvMazeScraperService : IHostedService, IDisposable
    {
        private readonly ILogger<TvMazeScraperService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITvMazeApiDbContext _context;
        private Timer _timer = null!;

        public TvMazeScraperService(ILogger<TvMazeScraperService> logger, IHttpClientFactory httpClientFactory, IServiceScopeFactory provider)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = provider.CreateScope().ServiceProvider.GetRequiredService<TvMazeApiDbContext>(); ;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var client = _httpClientFactory.CreateClient("TvMaze");
            var response = await client.GetAsync("shows");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var tvMazeApiDto = TvMazeApiShowResponseDto.FromJson(responseContent);
            //var listShows2014AndAbove = tvMazeApiDto.Where(x => x.Premiered >= new DateTimeOffset(2014, 01, 01, 0, 0, 0, new TimeSpan(0, 0, 0))).OrderBy(x => x.Premiered).ToList();
            var listShows2014AndAbove = tvMazeApiDto.Where(x => x.Premiered >= new DateTime(2014, 01, 01)).OrderBy(x => x.Premiered).ToList();

            foreach (var show in tvMazeApiDto)
            {
                _logger.LogInformation("TvMazeShowDto: {ShowPremieredDate}", show.Premiered);
            }

            foreach (var show in listShows2014AndAbove)
            {
                var genreList = new List<Genre>();

                foreach (var genre in show.Genres)
                {
                    var newGenre = new Genre
                    {
                        GenreType = genre,
                    };
                    genreList.Add(newGenre);
                }

                //_logger.LogInformation("Show premeried: {@showPremiered}", show);
                var tvShowEntity = new TvShow(show.Name, show.Language.ToString(), show.Premiered, genreList, show.Summary, show.Rating, show.Id);
                _logger.LogError("ShowEntity: {@Show}", tvShowEntity);
                await _context.TvMazeShows.AddAsync(tvShowEntity);
            }
            await _context.SaveChangesAsync(new CancellationToken());

            var allDbShows = _context.TvMazeShows.ToList();

            foreach (var show in allDbShows)
                _logger.LogCritical("DbShow: {ShowName}, {ShowRating}, {ShowPremiered}, {ShowGenreType}", show.Name, show.Rating.ToString(), show.Premiered, show.Genres);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TvMaze Scraper Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            _logger.LogInformation("TvMaze Scraper Service is stopped.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
