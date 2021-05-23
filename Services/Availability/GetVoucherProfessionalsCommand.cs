using System;
using System.Collections.Generic;
using MediatR;

namespace Services.Availability
{
    public class GetVoucherProfessionalsCommand : IRequest<IEnumerable<Guid>>
    {
        public int VoucherId { get; set; }
    }
}
