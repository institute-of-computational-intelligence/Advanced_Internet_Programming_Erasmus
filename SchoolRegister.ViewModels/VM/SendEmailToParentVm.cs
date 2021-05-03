using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class SendEmailToParentVm
    {
        public int SenderId {get; set;}
        public int StudentId {get; set;}
        public string Content {get; set;}       

        public string Title {get; set;}  

    }
} 