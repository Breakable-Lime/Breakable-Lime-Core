using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using BreakableLime.DockerBackgroundService.Handlers.Factories;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BreakableLime.DockerBackgroundService
{
    public class Worker : BackgroundService
    {
        private readonly IContainer _dependencyContainer;
        private readonly ILogger _logger;

        private readonly CreateContainerHandlerFactory _createContainerHandlerFactory;
        private readonly IDockerWorkQueue _dockerWorkQueue;

        public Worker(IDockerHostedServiceDependencyContainerBuilder dependencyContainerBuilder)
        {
            dependencyContainerBuilder.AddServices();
            _dependencyContainer = dependencyContainerBuilder.BuildContainer();
            
            using var dependencyLifetimeScope = _dependencyContainer.BeginLifetimeScope();
            
            _logger = dependencyLifetimeScope.Resolve<ILogger>();
            _createContainerHandlerFactory = dependencyLifetimeScope.Resolve<CreateContainerHandlerFactory>();
            _dockerWorkQueue = dependencyLifetimeScope.Resolve<IDockerWorkQueue>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //go through que
                    //fetch factories
                    //create handler
                    //execute
                DockerWorkItem item;
                while (_dockerWorkQueue.Dequeue(out item))
                { 
                    IDockerActionHandlerFactory factory = item.SpecificationsMarker switch
                    {
                        CreateContainerSpecification => _createContainerHandlerFactory,
                        _ => null
                    };

                    if (factory == null)
                        continue;

                    var handler = factory.Create(item);
                    handler.Execute();
                }
                
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
