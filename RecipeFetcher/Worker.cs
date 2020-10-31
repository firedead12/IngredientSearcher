using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IngredientSearcher.DataAccess;
using IngredientSearcher.RecipeFetcher.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IngredientSearcher.RecipeFetcher
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IServiceProvider _serviceProvider;
        private ServiceResolver _serviceResolve;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, ServiceResolver serviceResolver)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _serviceResolve = serviceResolver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<RecipeContext>();
                    foreach (var provider in context.Providers)
                    {
                        await _serviceResolve(provider.Title).FetchRecipes(provider.Api);
                    }
                }
                await Task.Delay(10000000, stoppingToken);
            }
        }
    }
    
    
}