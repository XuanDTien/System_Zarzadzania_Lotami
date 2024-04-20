using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System_Zarzadzania_Lotami.Controllers;
using System_Zarzadzania_Lotami.Data.DTO;
using System_Zarzadzania_Lotami.Services;
using Assert = Xunit.Assert;

namespace UnitTest
{
    public class FlightsControllerTests
    {
        private readonly Mock<IFlightService> _mockFlightService;
        private readonly FlightsController _controller;

        public FlightsControllerTests()
        {
            _mockFlightService = new Mock<IFlightService>();
            _controller = new FlightsController(_mockFlightService.Object);
        }

        [Fact]
        public void PostFlight_CheckWhenIsUnauthorize()
        {
            FlightDTO flightDTO = new()
            {
                FlightNumber = 104,
                DepartureDate = new DateTime(2024, 5, 20, 14, 30, 0),
                DepartureFrom = "Gdansk",
                ArrivalTo = "Krakow",
                PlaneType = "Boeing"
            };

            ActionResult<FlightDTO> result = _controller.PostFlight(flightDTO);

            _ = Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetFlights_ReturnsOkResult_WithFlights()
        {
            List<FlightDTO> flights =
            [
                new FlightDTO
                {
                    FlightNumber = 101,
                    DepartureDate = new DateTime(2024, 5, 7, 8, 0, 0),
                    DepartureFrom = "Warsaw",
                    ArrivalTo = "Gdansk",
                    PlaneType = "Boeing"
                },
                new FlightDTO
                {
                    FlightNumber = 102,
                    DepartureDate = new DateTime(2024, 5, 3, 9, 0, 0),
                    DepartureFrom = "Gdansk",
                    ArrivalTo = "Krakow",
                    PlaneType = "Boeing"
                }
            ];

            _ = _mockFlightService.Setup(service => service.GetFlights()).Returns(flights);

            ActionResult<IEnumerable<FlightDTO>> result = _controller.GetFlights();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            IEnumerable<FlightDTO> returnedFlights = Assert.IsAssignableFrom<IEnumerable<FlightDTO>>(okResult.Value);
            Assert.Equal(flights, returnedFlights);
        }

        [Fact]
        public void GetFlightById_ReturnsOkResult_WithFlight()
        {
            FlightDTO flightDTO = new() { Id = 1 };
            _ = _mockFlightService.Setup(service => service.GetFlightById(1)).Returns(flightDTO);

            ActionResult<FlightDTO> result = _controller.GetFlight(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            FlightDTO returnedFlight = Assert.IsType<FlightDTO>(okResult.Value);
            Assert.Equal(flightDTO, returnedFlight);
        }

        [Fact]
        public void PostFlight_ReturnsBadRequest_WhenFlightNumberIsNotUnique()
        {
            Mock<IFlightService> mockFlightService = new();
            FlightsController controller = new(mockFlightService.Object);

            FlightDTO flightDTO = new()
            {
                FlightNumber = 4534,
                DepartureDate = new DateTime(2024, 5, 20, 14, 30, 0),
                DepartureFrom = "Gdansk",
                ArrivalTo = "Krakow",
                PlaneType = "Boeing"
            };

            _ = mockFlightService.Setup(service => service.ValidateFlightDTO(It.IsAny<FlightDTO>())).Returns((false, "Flight number must be unique."));

            ActionResult<FlightDTO> result = controller.PostFlight(flightDTO);

            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Flight number must be unique.", badRequestResult.Value);
        }

        [Fact]
        public void PostFlight_ReturnsOkResult_WhenFlightIsCreatedSuccessfully()
        {
            FlightDTO flightDTO = new()
            {
                FlightNumber = 4934,
                DepartureDate = new DateTime(2024, 5, 23, 14, 30, 0),
                DepartureFrom = "Warsaw",
                ArrivalTo = "Krakow",
                PlaneType = "Embraer"
            };

            _ = _mockFlightService.Setup(service => service.ValidateFlightDTO(It.IsAny<FlightDTO>())).Returns((true, string.Empty));
            _ = _mockFlightService.Setup(service => service.CreateFlight(flightDTO)).Returns(flightDTO);

            ActionResult<FlightDTO> result = _controller.PostFlight(flightDTO);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            FlightDTO returnedFlight = Assert.IsType<FlightDTO>(okResult.Value);
            Assert.Equal(flightDTO, returnedFlight);
        }

        [Fact]
        public void PutFlight_ReturnsOkResult_WhenFlightIsUpdatedSuccessfully()
        {
            FlightDTO flightDTO = new()
            {
                Id = 1,
                FlightNumber = 4534,
                DepartureDate = new DateTime(2024, 4, 20, 8, 30, 0),
                DepartureFrom = "Warsaw",
                ArrivalTo = "Krakow",
                PlaneType = "Boeing"
            };

            _ = _mockFlightService.Setup(service => service.ValidateFlightDTO(flightDTO)).Returns((true, string.Empty));
            _ = _mockFlightService.Setup(service => service.UpdateFlight(flightDTO)).Returns(flightDTO);

            OkObjectResult? actionResult = _controller.PutFlight(1, flightDTO) as OkObjectResult;

            Assert.NotNull(actionResult);
            _ = Assert.IsType<OkObjectResult>(actionResult);

            FlightDTO? result = actionResult.Value as FlightDTO;
            Assert.NotNull(result);
            _ = Assert.IsType<FlightDTO>(result);

            Assert.Equal(flightDTO.Id, result.Id);
            Assert.Equal(flightDTO.FlightNumber, result.FlightNumber);
            Assert.Equal(flightDTO.DepartureDate, result.DepartureDate);
            Assert.Equal(flightDTO.DepartureFrom, result.DepartureFrom);
            Assert.Equal(flightDTO.ArrivalTo, result.ArrivalTo);
            Assert.Equal(flightDTO.PlaneType, result.PlaneType);
        }

        [Fact]
        public void DeleteFlight_ReturnsOkResult_WhenFlightIsDeletedSuccessfully()
        {
            int flightId = 1;

            _ = _mockFlightService.Setup(service => service.GetFlightById(flightId)).Returns(new FlightDTO { Id = flightId });

            OkResult? actionResult = _controller.DeleteFlight(flightId) as OkResult;

            Assert.NotNull(actionResult);
            _ = Assert.IsType<OkResult>(actionResult);

            _mockFlightService.Verify(service => service.DeleteFlight(flightId), Times.Once);
        }

        [Fact]
        public void DeleteFlight_ReturnsNotFoundResult_WhenFlightDoesNotExist()
        {
            int flightId = 1;

            _ = _mockFlightService.Setup(service => service.GetFlightById(flightId)).Returns((FlightDTO)null);

            NotFoundResult? actionResult = _controller.DeleteFlight(flightId) as NotFoundResult;

            Assert.NotNull(actionResult);
            _ = Assert.IsType<NotFoundResult>(actionResult);

            _mockFlightService.Verify(service => service.DeleteFlight(It.IsAny<int>()), Times.Never);
        }

    }
}
