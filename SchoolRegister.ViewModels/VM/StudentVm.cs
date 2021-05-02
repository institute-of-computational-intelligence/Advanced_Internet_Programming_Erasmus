using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class StudentVm
    {
        public double AverageGrade { get; set; }
        [Required]
        public int? GroupId { get; set; }
        public int? ParentId { get; set; }
        public string GroupName {get; set;}
        public int? Id { get; set; }        
    }
}