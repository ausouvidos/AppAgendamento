using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Models;

namespace Services.Availability
{
    public class ReserveSpotCommand : IRequest<ApiResponse>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Mobile { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public string RecaptchaResponse { get; set; }
        [Required]
        public string Voucher { get; set; }
    }
}
