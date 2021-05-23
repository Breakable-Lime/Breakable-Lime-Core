using System;
using System.Threading.Tasks;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public interface IDockerActionHandler
    {
        public Task Execute();
    }
}