using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.Services.Services;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolRegister.Services.Services {
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
        {
            if (filterPredicate == null ) throw new ArgumentNullException ($"filterPredicate is null");
            var studentEntity =DbContext.Users.OfType<Student> ().FirstOrDefault (filterPredicate);
            var studentVm = Mapper.Map<StudentVm> (studentEntity);
            return studentVm;
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null)
        {
            var studentEntities = DbContext.Students.AsQueryable ();
                if (filterPredicate != null)
                    studentEntities = studentEntities.Where (filterPredicate);
                var studentVms = Mapper.Map<IEnumerable<StudentVm>> (studentEntities);
                return studentVms;
        }
    }
}