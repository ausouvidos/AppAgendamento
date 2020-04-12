using System;
using MediatR;

namespace Services.Identity
{
    public class EnsureUserCommand : IRequest<bool>
    {
        public Guid UserIdentityId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
