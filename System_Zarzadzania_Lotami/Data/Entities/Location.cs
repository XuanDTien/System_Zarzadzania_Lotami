using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace System_Zarzadzania_Lotami.Data.Entity
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CityName { get; set; }

        public ICollection<Flight> DepartureFlights { get; set; }
        public ICollection<Flight> ArrivalFlights { get; set; }
    }
}
