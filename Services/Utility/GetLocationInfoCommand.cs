using MediatR;
using Models;

namespace Services.Utility
{
    public class GetLocationInfoCommand : IRequest<Location>
    {
        public string IP { get; set; }
    }
}
