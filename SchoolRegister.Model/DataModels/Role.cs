using Microsoft.AspNetCore.Identity;
using System;

namespace SchoolRegister.Model.DataModels
{
    public class Role : IdentityRole<int>
    {
        public RoleValue RoleValue { get; set; }

        public void Role()
        {
  
        }

        public void Role(string name, RoleValue roleValue)
        {
            
        }
    }
}