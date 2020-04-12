using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models.Identity;

namespace Services.Identity
{
    public class EnsureUserCommandHandler : IRequestHandler<EnsureUserCommand, bool>
    {
        private readonly AusOuvidosContext _db;

        public EnsureUserCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }
        public async Task<bool> Handle(EnsureUserCommand request, CancellationToken cancellationToken)
        {
            var userData = await _db.Users.FindAsync(request.UserIdentityId);
            if (userData == null)
            {
                await _db.Users.AddAsync(new User
                {
                    Id = request.UserIdentityId,
                    Email = request.UserEmail,
                    Name = request.UserName
                });
                await _db.SaveChangesAsync();
            }
            return true;
        }
    }
}
