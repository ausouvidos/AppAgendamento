using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models.Identity;

namespace Services.Identity
{
    public class IsAdminUserCommandHandler : IRequestHandler<IsAdminUserCommand, bool>
    {
        private readonly AusOuvidosContext _db;

        public IsAdminUserCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }
        public async Task<bool> Handle(IsAdminUserCommand request, CancellationToken cancellationToken)
        {
            var userData = await _db.Users.FindAsync(request.UserIdentityId);
            return userData?.IsAdmin ?? false;
        }
    }
}
