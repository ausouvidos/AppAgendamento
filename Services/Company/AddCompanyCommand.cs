using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Models;

namespace Services.Company
{
    public class AddCompanyCommand : IRequest<ApiResponse>
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        public string ContactPersonEmail { get; set; }

        public string RecaptchaResponse { get; set; }
    }
}
