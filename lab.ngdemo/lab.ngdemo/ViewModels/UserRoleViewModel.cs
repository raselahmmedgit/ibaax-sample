﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngdemo.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<AssignRoleModel> AssignRoles { get; set; }
    }
}