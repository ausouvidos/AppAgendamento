using MediatR;
using Models;

namespace Services.Team
{
    public class GetTeamMemberPhotoCommand : IRequest<Image>
    {
        public int MemberId { get; set; }
    }
}
