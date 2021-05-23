using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Repository.Models;
using MediatR;

namespace BreakableLime.Mediatr.requests.user
{
    public class UserByIdRequest : IRequest<Result<ApplicationIdentityUser>>
    {
        public string UserId { get; set; }
    }
}