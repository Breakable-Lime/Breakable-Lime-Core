using System;
using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;
using MediatR;

namespace BreakableLime.Mediatr.requests.image
{
    public class FetchImageRequest : IRequest<Result<Empty>>
    {
        public Uri RepositoryUri { get; set; }
        public string OwnerId { get; set; }
    }
}