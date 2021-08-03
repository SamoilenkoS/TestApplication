using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer.Models
{
    [Keyless]
    public class UserRoles// TODO rename it and rename UserWithRoles classes
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
