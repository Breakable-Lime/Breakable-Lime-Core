using System.Collections.Concurrent;
using System.Threading;

namespace BreakableLime.DockerBackgroundService.models.external
{
    public class DockerWorkQueue : IDockerWorkQueue
    {
        public DockerWorkQueue()
        {
            _queue = new ConcurrentQueue<DockerWorkItemBase>();
        }
        
        private readonly ConcurrentQueue<DockerWorkItemBase> _queue;
        
        public CancellationToken Enqueue(DockerWorkItemBase workItemBase)
        {
            var token = new CancellationToken();
            var item = workItemBase with {CancellationToken = token};
            
            _queue.Enqueue(item);
            
            return token;
        }

        public bool Dequeue(out DockerWorkItemBase workItemBase)
        {
            return _queue.TryDequeue(out workItemBase);
        }
    }
}