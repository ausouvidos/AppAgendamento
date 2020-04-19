using System.Collections.Generic;
using System.Linq;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Availability
{
    public class GetSpecificAvailabilityCommandHandler : RequestHandler<GetSpecificAvailabilityCommand, Models.Availability>
    {
        private readonly AusOuvidosContext _db;

        public GetSpecificAvailabilityCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }
        protected override Models.Availability Handle(GetSpecificAvailabilityCommand request) =>
             _db
                .Availabilities
                .Include(a => a.User)
                .Where(a => a.Start >= request.StartDate && a.End <= request.EndDate && a.IsFree)
                .FirstOrDefault();

    }
}
