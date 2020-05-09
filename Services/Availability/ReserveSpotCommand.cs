using System;
using MediatR;
using Models;

namespace Services.Availability
{
    public class ReserveSpotCommand : IRequest<ApiResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string RecaptchaResponse { get; set; }

        public string Voucher { get; set; }
    }
}
