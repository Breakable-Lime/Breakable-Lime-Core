using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BreakableLime.DockerBackgroundService
{
    public class Worker : BackgroundService
    {
        private readonly IContainer _dependencyContainer;
        private readonly ILogger _logger;

        public Worker(IDockerHostedServiceDependencyContainerBuilder dependencyContainerBuilder)
        {
            dependencyContainerBuilder.AddServices();
            _dependencyContainer = dependencyContainerBuilder.BuildContainer();
            
            using var dependencyLifetimeScope = _dependencyContainer.BeginLifetimeScope();
            _logger = dependencyLifetimeScope.Resolve<ILogger>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
