using System;
using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;
using MediatR;

namespace BreakableLime.Mediatr.requests.image
{
    public class FetchImageRequest : IRequest<Result<Empty>>
    {
        public string ImageName { get; set; }
        public string ImageTag { get; set; }
        public string OwnerId { get; set; }
    }
}