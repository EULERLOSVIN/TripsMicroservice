using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripsMicroservice.Features.Commands;

namespace TripsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TripsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestTrip([FromBody] CreateTripCommand command)
        {
            try
            {
                // El controlador delega TODO al Handler
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}