using Microsoft.AspNetCore.Mvc;
using System_Zarzadzania_Lotami.Data.DTO;
using System_Zarzadzania_Lotami.Services;

namespace System_Zarzadzania_Lotami.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController(IFlightService flightService) : ControllerBase
    {
        private readonly IFlightService _flightService = flightService;

        [HttpGet]
        public ActionResult<IEnumerable<FlightDTO>> GetFlights()
        {
            return Ok(_flightService.GetFlights());
        }

        [HttpGet("{id}")]
        public ActionResult<FlightDTO> GetFlight(int id)
        {
            var flight = _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [HttpPost]
        public ActionResult<FlightDTO> PostFlight([FromBody] FlightDTO flightDTO)
        {

            var (isValid, errorMessage) = _flightService.ValidateFlightDTO(flightDTO);
            if (!isValid)
            {
                return BadRequest(errorMessage);
            }

            var flight = _flightService.CreateFlight(flightDTO);

            return Ok(flight);
        }

        [HttpPut("{id}")]
        public IActionResult PutFlight(int id, [FromBody] FlightDTO flightDTO)
        {
            var (isValid, errorMessage) = _flightService.ValidateFlightDTO(flightDTO, id);
            if (!isValid)
            {
                return BadRequest(errorMessage);
            }

            flightDTO.Id = id;
            var flight = _flightService.UpdateFlight(flightDTO);
            if (flight == null)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            _flightService.DeleteFlight(id);
            return Ok();
        }
    }
}
