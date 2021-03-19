using Microsoft.AspNetCore.Identity;
using System;

namespace SchoolRegister.Model.DataModels
{
    public class Role : Identity<int>
    {
        public RoleValue RoleValue { get; set; }

        public Role()
        {

        }

        public Role(string name, RoleValue roleValue)
        {

        }

    }
}