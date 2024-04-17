using Microsoft.EntityFrameworkCore;
using System_Zarzadzania_Lotami.Data;
using System_Zarzadzania_Lotami.Data.DTO;
using System_Zarzadzania_Lotami.Data.Entity;

namespace System_Zarzadzania_Lotami.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightSystemContext _context;

        public FlightService(FlightSystemContext context)
        {
            _context = context;
        }
        public FlightDTO CreateFlight(FlightDTO flightDTO)
        {
            var flight = new Flight
            {
                FlightNumber = flightDTO.FlightNumber,
                DepartureDate = flightDTO.DepartureDate,
                DepartureFrom = GetLocationIdByName(flightDTO.DepartureFrom),
                ArrivalTo = GetLocationIdByName(flightDTO.ArrivalTo),
                PlaneTypeId = GetPlaneTypeIdByName(flightDTO.PlaneType)
            };

            _context.Flights.Add(flight);
            _context.SaveChanges();
            flightDTO.Id = flight.Id;
            return flightDTO;
        }

        public void DeleteFlight(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }
        }

        public FlightDTO? GetFlightById(int id)
        {
            var result = _context.Flights.Where(x => x.Id == id).Select(x => new FlightDTO
            {
                FlightNumber = x.FlightNumber,
                DepartureDate = x.DepartureDate,
                DepartureFrom = x.DepartureLocation.CityName,
                ArrivalTo = x.ArrivalLocation.CityName,
                PlaneType = x.PlaneType.TypeName
            }).FirstOrDefault(); 

            return result;
        }

        public IEnumerable<FlightDTO> GetFlights()
        {

            return _context.Flights
                .Select(x => new FlightDTO
            {
                Id = x.Id,
                FlightNumber = x.FlightNumber,
                DepartureDate = x.DepartureDate,
                DepartureFrom = x.DepartureLocation.CityName,
                ArrivalTo = x.ArrivalLocation.CityName,
                PlaneType = x.PlaneType.TypeName
            })
            .ToArray();
        }

        public FlightDTO UpdateFlight(FlightDTO flightDTO)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == flightDTO.Id);
            if (flight != null)
            {
                flight.DepartureDate = flightDTO.DepartureDate;
                flight.DepartureFrom = GetLocationIdByName(flightDTO.DepartureFrom);
                flight.ArrivalTo = GetLocationIdByName(flightDTO.ArrivalTo);
                flight.PlaneTypeId = GetPlaneTypeIdByName(flightDTO.PlaneType);

                _context.SaveChanges();

                return flightDTO;
            }

            return null;
        }

        public int GetLocationIdByName(string locationName)
        {
            var location = _context.Locations.FirstOrDefault(l => l.CityName.ToLower() == locationName.ToLower());

            if (location == null)
            {
                location = new Location { CityName = locationName };
                _context.Locations.Add(location);
                _context.SaveChanges();
            }
            return location.Id;
        }

        public int GetPlaneTypeIdByName(string type)
        {
            var planeType = _context.Types.FirstOrDefault(t => t.TypeName.ToLower() == type.ToLower());
            if (planeType == null)
            {
                planeType = new PlaneType { TypeName = type};
                _context.Types.Add(planeType);
                _context.SaveChanges();
            }
            return planeType.Id;
        }

        public (bool isValid, string errorMessage) ValidateFlightDTO(FlightDTO flightDTO, int? existingFlightId = null)
        {
            var isUnique = existingFlightId.HasValue
            ? IsFlightNumberUnique(flightDTO.FlightNumber, existingFlightId.Value)
            : IsFlightNumberUnique(flightDTO.FlightNumber);


            if (IsFlightNumberUnique(flightDTO.FlightNumber))
            {
                if (flightDTO.DepartureDate >= DateTime.Now)
                {
                    if (flightDTO.DepartureFrom.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) && 
                        flightDTO.ArrivalTo.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    {
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Departure and Arrival locations must contain only letters.");
                    }
                }
                else
                {
                    return (false, "Departure date cannot be in the past.");
                }
            }
            else
            {
                return (false, "Flight number must be unique.");
            }
        }
        public bool IsFlightNumberUnique(int flightNumber, int? existingFlightId = null)
        {
            if(existingFlightId.HasValue)
                return !_context.Flights.Any(f => f.FlightNumber == flightNumber && f.Id != existingFlightId.Value);

            return !_context.Flights.Any(f => f.FlightNumber == flightNumber);
        }
    }
}
