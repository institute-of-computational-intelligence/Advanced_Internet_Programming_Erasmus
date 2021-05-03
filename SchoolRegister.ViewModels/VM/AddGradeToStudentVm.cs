using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class AddGradeToStudentVm
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public GradeScale GradeValue { get; set; }

    }
} 