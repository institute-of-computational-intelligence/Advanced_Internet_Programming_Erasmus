using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Services
{
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSenderService _emailService;
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager, IEmailSenderService emailService) : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            if (filterPredicate == null) throw new ArgumentNullException($"filterPredicate is null");
            var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
            var teacherVm = Mapper.Map<TeacherVm>(teacherEntity);
            return teacherVm;
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
        {
            var teacherEntities = DbContext.Teachers.AsQueryable();
            if (filterPredicate != null)
                teacherEntities = teacherEntities.Where(filterPredicate);
            var teacherVms = Mapper.Map<IEnumerable<TeacherVm>>(teacherEntities);
            return teacherVms;
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            if (getTeachersGroups == null)
            {
                throw new ArgumentNullException($"Vm is null");
            }
            var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(x => x.Id == getTeachersGroups.TeacherId);
            var teacherGroups = teacherEntity?.Subjects.SelectMany(s => s.SubjectGroups.Select(gr => gr.Group));
            var teacherGroupsVm = Mapper.Map<IEnumerable<GroupVm>>(teacherGroups);
            return teacherGroupsVm;
        }

        public async Task<bool> SendEmailToParentAsync(SendEmailToParentVm sendEmailToParentVm)
        {
            try
            {
                if (sendEmailToParentVm == null)
                {
                    throw new ArgumentNullException($"Vm is null");
                }

                var teacher = DbContext.Users.OfType<Teacher>()
                .FirstOrDefault(x => x.Id == sendEmailToParentVm.SenderId);
                if (teacher == null || _userManager.IsInRoleAsync(teacher, "Teacher").Result == false)
                {
                    throw new InvalidOperationException("sender is not teacher");
                }

                var student = DbContext.Users.OfType<Student>().FirstOrDefault(x => x.Id == sendEmailToParentVm.StudentId);
                if (student == null || !_userManager.IsInRoleAsync(student, "Student").Result)
                {
                    throw new InvalidOperationException("given user is not student");
                }
                await _emailService.SendEmailAsync(student.Parent.Email, teacher.Email, sendEmailToParentVm.Title, sendEmailToParentVm.Content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}