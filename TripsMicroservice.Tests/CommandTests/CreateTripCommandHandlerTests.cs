using Xunit;
using System;
using System.Linq;
using TripsMicroservice.Features.Commands;
using TripsMicroservice.Tests.Fixtures;
using System.Threading;

namespace TripsMicroservice.Tests.CommandTests
{
    // Solo probaremos el lado de escritura (POST)
    public class CreateTripCommandHandlerTests
    {
        [Fact]
        public async Task CreateTripCommand_DebeGuardarViajeYTarifaEnBD()
        {
            // 1. PREPARAR (ARRANGE)
            using var context = TestingDbContextFactory.Create();
            var handler = new CreateTripCommandHandler(context);

            var command = new CreateTripCommand
            {
                PassengerId = 15,
                OriginLat = -12.0m,
                OriginLng = -77.0m,
                DestLat = -12.1m,
                DestLng = -77.1m,
                OriginAddress = "Inicio",
                DestinationAddress = "Fin"
            };

            // 2. EJECUTAR (ACT)
            var result = await handler.Handle(command, CancellationToken.None);

            // 3. VERIFICAR (ASSERT)

            // Verifica Persistencia: Confirmamos que el registro se creó en la BD Falsa
            var tripInDb = context.Trips.SingleOrDefault();
            Assert.NotNull(tripInDb);
            Assert.Equal(1, tripInDb.IdTripStates);

            // Verifica Lógica: El Handler debe devolver un ID válido
            Assert.True(result.TripId > 0);
        }

        [Fact]
        public async Task CreateTripCommand_DebeLanzarExcepcionSiIdPasajeroEsInvalido()
        {
            // Prueba de Regla de Negocio: ID <= 0 debe fallar
            using var context = TestingDbContextFactory.Create();
            var handler = new CreateTripCommandHandler(context);
            var command = new CreateTripCommand { PassengerId = 0 };

            // Esperamos que el código falle con una excepción
            await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None)
            );
        }
    }
}