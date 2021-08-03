using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class AuthenticationModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public  string Login { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public  string Password { get; set; }
    }
}
