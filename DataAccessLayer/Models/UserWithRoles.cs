using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class UserWithRoles
    {
        public Guid UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
