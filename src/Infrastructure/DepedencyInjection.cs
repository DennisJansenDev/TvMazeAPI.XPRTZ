﻿using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TvMazeApiDbContext>(options => options.UseInMemoryDatabase("TvMazeDb"));

            services.AddHttpClient("TvMaze", httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["AppSettings:TvMazeBaseUri"]);
            });
            services.AddHostedService<GetShowsTimedService>();
            return services;
        }

    }
}
