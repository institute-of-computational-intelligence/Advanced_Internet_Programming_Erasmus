using System;
using Microsoft.AspNetCore.Identity;


namespace SchoolRegister.Model.DataModels
{
    public class Role : IdentityUser<int>
    {   
       
        public RoleValue RoleValue {get; set;}
        public Role(){}
        public Role(string name, RoleValue roleValue){}
    }
}