using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TripsMicroservice.Controllers;
using TripsMicroservice.Features.Commands;
using Xunit;

namespace TripsMicroservice.Tests.Controllers
{
    public class TripsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TripsController _controller;

        public TripsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TripsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task RequestTrip_ShouldReturnOk_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreateTripCommand
            {
                PassengerId = 1,
                OriginAddress = "Origin",
                DestinationAddress = "Destination",
                OriginLat = 10.0,
                OriginLng = 20.0,
                DestLat = 30.0,
                DestLng = 40.0
            };

            var expectedResponse = new TripResponse
            {
                TripId = 1,
                EstimatedFare = 10.0m,
                Message = "Success"
            };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.RequestTrip(command);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedResponse);
            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RequestTrip_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var command = new CreateTripCommand { PassengerId = 1 };
            var errorMessage = "Error processing request";

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.RequestTrip(command);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            // Using a simpler assertion for the anonymous object
            badRequestResult.Value.ToString().Should().Contain(errorMessage);
        }
    }
}
