using System.Collections.Generic;
using System.Linq;
using Data;
using MediatR;

namespace Services.Availability
{
    public class GetAvailabilitiesCommandHandler : RequestHandler<GetAvailabilitiesCommand, IEnumerable<Models.Availability>>
    {
        private readonly AusOuvidosContext _db;

        public GetAvailabilitiesCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }
        protected override IEnumerable<Models.Availability> Handle(GetAvailabilitiesCommand request) =>
             _db.Availabilities.Where(a => a.UserId == request.UserIdentityId).ToList();
    }
}
