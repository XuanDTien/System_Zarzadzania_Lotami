using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace System_Zarzadzania_Lotami.Data.Entity
{
    public class PlaneType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }
}
