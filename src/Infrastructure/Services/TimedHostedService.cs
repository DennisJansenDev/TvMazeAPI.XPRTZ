using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.Dto;

namespace Infrastructure.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private Timer _timer = null!;

        public TimedHostedService(ILogger<TimedHostedService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
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
            var list2014 = tvMazeApiDto.Where(x => x.Premiered >= new DateTimeOffset(2014, 01, 01, 0, 0, 0, new TimeSpan(0, 0, 0))).OrderByDescending(x => x.Premiered).ToList();

            foreach (var item in list2014)
            {
                _logger.LogInformation(item.Premiered.ToString());
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
