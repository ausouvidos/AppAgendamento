using System;
using System.Collections.Generic;
using MediatR;
using Models;

namespace Services.Team
{
    public class GetTeamCommand : IRequest<IEnumerable<Profissional>>
    {
    }
}
