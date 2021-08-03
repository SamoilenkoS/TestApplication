using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
