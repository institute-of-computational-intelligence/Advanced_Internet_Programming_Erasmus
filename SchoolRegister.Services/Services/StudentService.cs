using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq;
using System.Threading.Tasks;
namespace SchoolRegister.Services.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null)
        {
            throw new NotImplementedException();
        }
    }
}