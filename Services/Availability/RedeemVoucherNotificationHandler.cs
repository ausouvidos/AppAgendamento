using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;

namespace Services.Availability
{
    public class RedeemVoucherNotificationHandler : INotificationHandler<RedeemVoucherNotification>
    {
        private readonly AusOuvidosContext _db;

        public RedeemVoucherNotificationHandler(AusOuvidosContext db)
        {
            this._db = db;
        }

        public Task Handle(RedeemVoucherNotification request, CancellationToken cancellationToken)
        {
            request.Voucher.RemainingRedeemCount--;
            _db.Vouchers.Update(request.Voucher);
            return Task.CompletedTask;
        }
    }
}
