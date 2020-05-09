using MediatR;
using Models;

namespace Services.Availability
{
    public class CompleteAvailabilityCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Observacoes { get; set; }
        public string Notas { get; set; }
    }
}
