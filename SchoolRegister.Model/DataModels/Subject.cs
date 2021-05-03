using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.Model.DataModels
{
    public class Subject
    {
        [Key]
        
        public string Description { get; set; }
        
        public IUserLoginStore<Grade> Grades { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<SubjectGroup> SubjectGroups { get; set; }

        public Teacher Teacher { get; set; }

        public int? TeacherId { get; set; }
    }
    
}