using System;
using BreakableLime.DockerBackgroundService.models.external;
using Microsoft.Extensions.Logging;
using Autofac;

namespace BreakableLime.DockerBackgroundService
{
    public interface IDockerHostedServiceDependencyContainerBuilder
    {
        void AddServices();
        IContainer BuildContainer();
    }

    public class DockerHostedServiceDependencyContainerBuilder : IDockerHostedServiceDependencyContainerBuilder
    {
        private readonly ILogger _sharedLogger;
        private readonly IDockerWorkQueue _sharedDockerWorkQueue;
        private readonly ContainerBuilder _containerBuilder;


        public DockerHostedServiceDependencyContainerBuilder(ILogger sharedLogger, IDockerWorkQueue sharedDockerWorkQueue)
        {
            _sharedLogger = sharedLogger;
            _sharedDockerWorkQueue = sharedDockerWorkQueue;

            _containerBuilder = new ContainerBuilder();
        }

        public void AddServices()
        {
            throw new NotImplementedException();
        }

        public IContainer BuildContainer() => _containerBuilder.Build();
    }
}