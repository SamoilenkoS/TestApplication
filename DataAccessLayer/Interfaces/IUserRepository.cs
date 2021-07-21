using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<string> GetUserRolesById(Guid userId);
    }
}
