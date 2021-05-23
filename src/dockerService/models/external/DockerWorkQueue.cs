using System.Collections.Concurrent;
using System.Threading;

namespace BreakableLime.DockerBackgroundService.models.external
{
    public class DockerWorkQueue : IDockerWorkQueue
    {
        public DockerWorkQueue()
        {
            _queue = new ConcurrentQueue<DockerWorkItem>();
        }
        
        private readonly ConcurrentQueue<DockerWorkItem> _queue;
        
        public CancellationToken Enqueue(DockerWorkItem workItem)
        {
            var token = workItem.CancellationToken == CancellationToken.None ? new CancellationToken() : workItem.CancellationToken;
            var item = workItem with {CancellationToken = token};
            
            _queue.Enqueue(item);
            
            return token;
        }

        public bool Dequeue(out DockerWorkItem workItem)
        {
            return _queue.TryDequeue(out workItem);
        }
    }
}