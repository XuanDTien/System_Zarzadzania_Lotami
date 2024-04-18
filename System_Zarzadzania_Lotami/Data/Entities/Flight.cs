using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace System_Zarzadzania_Lotami.Data.Entity
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int FlightNumber { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public int DepartureFrom { get; set; }
        [Required]
        public int ArrivalTo { get; set; }
        [Required]
        public int PlaneTypeId { get; set; }

        public Location DepartureLocation { get; set; }
        public Location ArrivalLocation { get; set; }
        public PlaneType PlaneType { get; set; }
    }
}
