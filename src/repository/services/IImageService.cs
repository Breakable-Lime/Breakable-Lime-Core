using System.Threading;
using System.Threading.Tasks;
using BreakableLime.Repository.Models;

namespace BreakableLime.Repository.services
{
    public interface IImageService //TODO: register with dependency container
    {
        public Task<bool> CreateImage(ContainerImage image, CancellationToken cancellationToken);
        public Task<bool> DeleteImage(ContainerImage image, CancellationToken cancellationToken);
        public Task<ContainerImage> EditImage(ContainerImage image, CancellationToken cancellationToken);
    }
}