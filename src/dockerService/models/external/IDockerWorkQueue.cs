using System.Runtime.CompilerServices;
using System.Threading;

namespace BreakableLime.DockerBackgroundService.models.external
{
    public interface IDockerWorkQueue
    {
        public CancellationToken Enqueue(DockerWorkItemBase workItemBase);
        public bool Dequeue(out DockerWorkItemBase workItemBase);
    }
}