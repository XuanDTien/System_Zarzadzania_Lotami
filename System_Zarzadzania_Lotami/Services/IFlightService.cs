using System_Zarzadzania_Lotami.Data.DTO;
using System_Zarzadzania_Lotami.Data.Entity;

namespace System_Zarzadzania_Lotami.Services
{
    public interface IFlightService
    {
        IEnumerable<FlightDTO> GetFlights();
        FlightDTO GetFlightById(int id);
        FlightDTO CreateFlight(FlightDTO flight);
        FlightDTO UpdateFlight(FlightDTO flight);
        void DeleteFlight(int id);
        (bool isValid, string errorMessage) ValidateFlightDTO(FlightDTO flightDTO);
    }
}
