﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRolesRepository
    {
        bool AddUserRole(AddUserRoleModel addUserRoleModel);
    }
}
