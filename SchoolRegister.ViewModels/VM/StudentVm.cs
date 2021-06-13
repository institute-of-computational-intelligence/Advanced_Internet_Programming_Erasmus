using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class StudentVm
    {
        public double AverageGrade { get; set; }
        public int? GroupId { get; set; }
        public int? ParentId { get; set; }
        public string GroupName {get; set;}
        public string UserName {get; set;}
        public string ParentName {get; set;}
        public int? Id { get; set; }        
    }
}