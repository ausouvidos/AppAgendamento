using MediatR;
using Models;

namespace Services.Partner
{
    public class GetPartnerLogoCommand : IRequest<Image>
    {
        public int Id { get; set; }
    }
}
