using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AttachDetachStudentToGroupVm
    {
        public int GroupId { get; set; }
        public int StudentId { get; set; }
        public int ParentId { get; set; }
        
    }
}