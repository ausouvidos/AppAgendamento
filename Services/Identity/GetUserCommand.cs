using System.Collections.Generic;
using MediatR;
using Models.Identity;

namespace Services.Identity
{
    public class GetUserCommand : IRequest<IEnumerable<User>>
    {
    }
}
