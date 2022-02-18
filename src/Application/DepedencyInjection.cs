﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
