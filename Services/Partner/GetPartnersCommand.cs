using System.Collections.Generic;
using MediatR;

namespace Services.Partner
{
    public class GetPartnersCommand : IRequest<IEnumerable<Models.Partner>>
    {
    }
}
