using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Models.Identity;

namespace Services.Availability
{
    public class AddAvailabilitiesCommandHandler : IRequestHandler<AddAvailabilitiesCommand, bool>
    {
        private readonly AusOuvidosContext _db;

        public AddAvailabilitiesCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }
        public async Task<bool> Handle(AddAvailabilitiesCommand request, CancellationToken cancellationToken)
        {
            var availabilities = request?.Dates?.Select(a =>
            {
                return new Models.Availability
                {
                    IsFree = true,
                    Start = a.Start,
                    End = a.End,
                    UserId = request.UserIdentityId
                };
            }).ToArray();

            await _db.Availabilities.AddRangeAsync(availabilities);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
