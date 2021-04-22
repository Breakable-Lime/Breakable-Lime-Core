using System.Runtime.CompilerServices;
using System.Threading;

namespace BreakableLime.DockerBackgroundService.models.external
{
    public interface IDockerWorkQueue
    {
        public CancellationToken Enqueue(DockerWorkItem workItem);
        public bool Dequeue(out DockerWorkItem workItem);
    }
}