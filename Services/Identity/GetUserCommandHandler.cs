using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data;
using MediatR;
using Models.Identity;
using System.Linq;

namespace Services.Identity
{
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, IEnumerable<User>>
    {
        private readonly AusOuvidosContext _db;

        public GetUserCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }

        public Task<IEnumerable<User>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var users = _db.Users.OrderBy(u => u.Name);
            return Task.FromResult(users.AsEnumerable());
        }
    }
}
