using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using MediatR;
using Services.Utility;

namespace Services.Availability
{
    public class CanReserveTodayCommandHandler : IRequestHandler<CanReserveTodayCommand, bool>
    {
        private readonly AusOuvidosContext _db;

        public CanReserveTodayCommandHandler(AusOuvidosContext db)
        {
            this._db = db;
        }

        public Task<bool> Handle(CanReserveTodayCommand request, CancellationToken cancellationToken)
        {
            DateTime daysAgo = DateTime.Now.Date.AddHours(-72);
            return Task.FromResult(!_db.Availabilities.Any(a => a.CustomerEmail == request.Email && a.Start > daysAgo));
        }
    }
}
