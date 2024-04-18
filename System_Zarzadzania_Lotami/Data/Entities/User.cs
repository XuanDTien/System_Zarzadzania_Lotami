using System.ComponentModel.DataAnnotations;

namespace System_Zarzadzania_Lotami.Data.Entities
{
    public class User
    {
        [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
