using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolRegister.Model.DataModels
{
    public class SubjectGroup
    {
        public virtual Group Group {get; set;}
        public int GroupId {get; set;}
        public virtual Subject Subject {get; set;}
        [ForeignKey("Subject")]
        public int SubjectId {get; set;}

    }
}