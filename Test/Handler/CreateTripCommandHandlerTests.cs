using Microsoft.EntityFrameworkCore;
using TripsMicroservice.Data;
using TripsMicroservice.Features.Commands;
using FluentAssertions;
using Moq;
using MassTransit;

namespace Test.Handler
{
    public class CreateTripCommandHandlerTests
    {
        private readonly TripsDbContext _context;
        private readonly Mock<IPublishEndpoint> _publishEndpointMock;
        private readonly CreateTripCommandHandler _handler;

        public CreateTripCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<TripsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TripsDbContext(options);
            _publishEndpointMock = new Mock<IPublishEndpoint>();
            _handler = new CreateTripCommandHandler(_context, _publishEndpointMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateTripAndFare_WhenRequestIsValid()
        {
            // Arrange
            var command = new CreateTripCommand
            {
                PassengerId = 1,
                OriginAddress = "Origin",
                DestinationAddress = "Destination",
                OriginLat = 10.0m,
                OriginLng = 20.0m,
                DestLat = 30.0m,
                DestLng = 40.0m
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Viaje solicitado vía CQRS Command");

            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == result.TripId);
            trip.Should().NotBeNull();
            trip!.OriginAddress.Should().Be(command.OriginAddress);
            trip.IdTripStates.Should().Be(1); // Pending

            var fare = await _context.TripFares.FirstOrDefaultAsync(f => f.IdTripNavigation == trip);
            fare.Should().NotBeNull();
            fare!.TotalFare.Should().Be(9.50m); // 5.00 + 4.50
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPassengerIdIsInvalid()
        {
            // Arrange
            var command = new CreateTripCommand { PassengerId = 0 };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("El ID del pasajero no es válido.");
        }
    }

}
