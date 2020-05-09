using System;
using MediatR;
using Models;

namespace Services.Availability
{
    public class GetVoucherCommand : IRequest<Voucher>
    {
        public string Voucher { get; set; }
    }
}
