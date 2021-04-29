using System;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public interface IDockerActionHandler
    {
        public void Execute();
    }
}