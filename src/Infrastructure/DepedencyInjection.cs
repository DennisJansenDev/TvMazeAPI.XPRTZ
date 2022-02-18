using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TvMazeApiDbContext>(options => options.UseInMemoryDatabase("TvMazeDb"));
            services.AddScoped<ITvMazeApiDbContext>(provider => provider.GetRequiredService<TvMazeApiDbContext>());

            services.AddHttpClient("TvMaze", httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["AppSettings:TvMazeBaseUri"]);
            });

            services.AddHostedService<TvMazeScraperService>();
            return services;
        }

    }
}
