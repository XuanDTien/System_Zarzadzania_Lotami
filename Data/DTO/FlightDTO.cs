namespace System_Zarzadzania_Lotami.Data.DTO
{
    public class FlightDTO
    {
        public int? Id { get; set; }
        public int FlightNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        //public string FormattedDepartureDate => DepartureDate.ToString("yyyy-MM-dd HH:mm:ss");
        public string DepartureFrom { get; set; }
        public string ArrivalTo { get; set; }
        public string PlaneType { get; set; }
    }
}
