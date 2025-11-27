using MediatR;
using TripsMicroservice.Data;
using TripsMicroservice.Entities;

namespace TripsMicroservice.Features.Commands
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripResponse>
    {
        private readonly TripsDbContext _context;

        public CreateTripCommandHandler(TripsDbContext context)
        {
            _context = context;
        }

        public async Task<TripResponse> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            // 1. REGLAS DE NEGOCIO / VALIDACIONES
            if (request.PassengerId <= 0)
                throw new Exception("El ID del pasajero no es válido.");

            // 2. LÓGICA: Calcular Tarifa (Tu algoritmo va aquí)
            decimal baseFare = 5.00m;
            // Aquí podrías usar Math.Sqrt para distancia real, pero usaremos fijo por ahora
            decimal distanceFare = 4.50m;
            decimal total = baseFare + distanceFare;

            // 3. MODIFICAR ESTADO: Crear Entidades
            var newTrip = new Trip
            {
                IdPassengerAccount = request.PassengerId,
                IdTripStates = 1, // Pending
                OriginAddress = request.OriginAddress,
                DestinationAddress = request.DestinationAddress,
                OriginLat = request.OriginLat,
                OriginLng = request.OriginLng,
                DestLat = request.DestLat,
                DestLng = request.DestLng,
                CreatedDate = DateTime.Now
            };

            var newFare = new TripFare
            {
                IdTripNavigation = newTrip, // Vinculación
                BaseFare = baseFare,
                DistanceFare = distanceFare,
                TimeFare = 0,
                TotalFare = total,
                CalculatedDate = DateTime.Now
            };

            // 4. PERSISTENCIA
            _context.Trips.Add(newTrip);
            _context.TripFares.Add(newFare);
            await _context.SaveChangesAsync(cancellationToken);

            // 5. RETORNO
            return new TripResponse
            {
                TripId = newTrip.IdTrip,
                EstimatedFare = total,
                Message = "Viaje solicitado vía CQRS Command"
            };
        }
    }
}