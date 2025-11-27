using MediatR;

namespace TripsMicroservice.Features.Commands
{
    // Devuelve un objeto anónimo o una clase con el ID y la Tarifa
    public class CreateTripCommand : IRequest<TripResponse>
    {
        public int PassengerId { get; set; }
        public string OriginAddress { get; set; } = string.Empty;
        public string DestinationAddress { get; set; } = string.Empty;
        public decimal OriginLat { get; set; }
        public decimal OriginLng { get; set; }
        public decimal DestLat { get; set; }
        public decimal DestLng { get; set; }
    }

    // Una clasecita simple para la respuesta
    public class TripResponse
    {
        public int TripId { get; set; }
        public decimal EstimatedFare { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}