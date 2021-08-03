using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class AddUserRoleModel
    {
        public Guid UserId { get; set; }
        public string RoleTitle { get; set; }
    }
}
