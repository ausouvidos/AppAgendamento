using System;
using MediatR;

namespace Services.Identity
{
    public class IsAdminUserCommand : IRequest<bool>
    {
        public Guid UserIdentityId { get; set; }
    }
}
