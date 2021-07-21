using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IUserService
    {
        IEnumerable<string> GetUserRolesById(Guid userId);
    }
}
