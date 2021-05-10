using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Company
{
    public class ApproveCompanyCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int[] Professionals { get; set; }
    }
}
