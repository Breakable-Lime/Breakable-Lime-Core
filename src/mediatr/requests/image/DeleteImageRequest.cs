using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;
using MediatR;

namespace BreakableLime.Mediatr.requests.image
{
    public class DeleteImageRequest : IRequest<Result<Empty>>
    {
        public string EntityId { get; set; }
    }
}