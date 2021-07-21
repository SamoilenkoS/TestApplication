using System;

namespace DataAccessLayer.Models
{
    public class UserWithRole
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
