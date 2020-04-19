using MediatR;

namespace Services.Availability
{
    public class ReservedSpotNotification : INotification
    {
        public Models.Availability Availability { get; set; }
    }
}
