using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateSubjectVm
    {
        public int? Id {get; set;}
        public string Name {get; set;}
        [Required]
        public string Description {get; set;}
        [Required]
        public int TeacherId {get; set;}
        
    }
}