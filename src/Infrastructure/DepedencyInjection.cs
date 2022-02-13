using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient("TvMaze", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.tvmaze.com");
            });
            services.AddHostedService<TimedHostedService>();
            return services;
        }

    }
}
