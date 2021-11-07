using MediatR;

namespace Services.Availability
{
    public class CanReserveTodayCommand: IRequest<bool>
    {
        public string Email { get; set; }
    }
}
