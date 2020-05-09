using MediatR;
using Models;

namespace Services.Availability
{
    public class RedeemVoucherNotification : INotification
    {
        public Voucher Voucher { get; set; }
    }
}
