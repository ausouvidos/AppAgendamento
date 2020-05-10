using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Company
{
    public class GetPendingApprovalCompaniesCommand : IRequest<IEnumerable<Models.Company>>
    {
       
    }
}
