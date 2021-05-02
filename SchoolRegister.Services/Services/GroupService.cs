using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

namespace SchoolRegister.Services.Services
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _userManager;
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            if (addOrUpdateGroupVm == null)
            {
                throw new ArgumentNullException($"Vm of type is null");
            }
            var groupEntity = Mapper.Map<Group>(addOrUpdateGroupVm);
            if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
                DbContext.Groups.Add(groupEntity);
            else
                DbContext.Groups.Update(groupEntity);
            DbContext.SaveChanges();
            var groupVm = Mapper.Map<GroupVm>(groupEntity);
            return groupVm;
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            if (attachStudentToGroupVm == null)
            {
                throw new ArgumentNullException($"Vm of type is null");
            }
            var student = DbContext.Students
            .FirstOrDefault(st => st.GroupId == attachStudentToGroupVm.GroupId && st.ParentId == attachStudentToGroupVm.ParentId);
            if (student != null)
            {
                throw new ArgumentNullException($"There is such attachment already defined.");
            }
            student = new Student
            {
                GroupId = attachStudentToGroupVm.GroupId,
                ParentId = attachStudentToGroupVm.ParentId
            };
            DbContext.Students.Add(student);
            DbContext.SaveChanges();
            var group = DbContext.Students.FirstOrDefault(x => x.Id == attachStudentToGroupVm.GroupId);
            var groupVm = Mapper.Map<StudentVm>(group);
            return groupVm;
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroup)
        {
            if (attachSubjectGroup == null)
            {
                throw new ArgumentNullException($"Vm of type is null");
            }
            var subjectGroup = DbContext.SubjectGroups
            .FirstOrDefault(sg => sg.GroupId == attachSubjectGroup.GroupId && sg.SubjectId == attachSubjectGroup.SubjectId);
            if (subjectGroup != null)
            {
                throw new ArgumentNullException($"There is such attachment already defined.");
            }
            subjectGroup = new SubjectGroup
            {
                GroupId = attachSubjectGroup.GroupId,
                SubjectId = attachSubjectGroup.SubjectId
            };
            DbContext.SubjectGroups.Add(subjectGroup);
            DbContext.SaveChanges();
            var group = DbContext.Groups.FirstOrDefault(x => x.Id == attachSubjectGroup.GroupId);
            var groupVm = Mapper.Map<GroupVm>(group);
            return groupVm;
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            if (attachDetachSubjectToTeacherVm == null)
            {
                throw new ArgumentNullException($"Vm of type is null");
            }
            var teacher = DbContext.Teachers
            .FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.TeacherId );
            if (teacher != null)
            {
                throw new ArgumentNullException($"There is such attachment already defined.");
            }
            teacher = new Teacher
            {
                Id = attachDetachSubjectToTeacherVm.TeacherId,
            };
            DbContext.Teachers.Add(teacher);
            DbContext.SaveChanges();
            var subject = DbContext.Subjects.FirstOrDefault(x => x.Id == attachDetachSubjectToTeacherVm.SubjectId);
            var subjectVm = Mapper.Map<SubjectVm>(subject);
            return subjectVm;
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            if (detachStudentToGroupVm == null)
            {
                throw new ArgumentNullException($"Vm of type is null");
            }
            var student = DbContext.Students
            .FirstOrDefault(sg => sg.GroupId == detachStudentToGroupVm.GroupId && sg.ParentId == detachStudentToGroupVm.ParentId);
            if (student == null)
            {
                throw new ArgumentNullException($"The is no such attachment between group and subject");
            }
            DbContext.Students.Remove(student);
            DbContext.Remove(student);
            DbContext.SaveChanges();
            var group = DbContext.Students.FirstOrDefault(x => x.Id == detachStudentToGroupVm.GroupId);
            var groupVm = Mapper.Map<StudentVm>(group);
            return groupVm;
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachDetachSubject)
        {
            if (detachDetachSubject == null)
            {
                throw new ArgumentNullException($"Vm of type is null");
            }
            var subjectGroup = DbContext.SubjectGroups
            .FirstOrDefault(sg => sg.GroupId == detachDetachSubject.GroupId && sg.SubjectId == detachDetachSubject.SubjectId);
            if (subjectGroup == null)
            {
                throw new ArgumentNullException($"The is no such attachment between group and subject");
            }
            DbContext.SubjectGroups.Remove(subjectGroup);
            DbContext.Remove(subjectGroup);
            DbContext.SaveChanges();
            var group = DbContext.Groups.FirstOrDefault(x => x.Id == detachDetachSubject.GroupId);
            var groupVm = Mapper.Map<GroupVm>(group);
            return groupVm;
        }



        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            if (attachDetachSubjectToTeacherVm == null)
            {
                throw new ArgumentNullException($"Vm of type is null");
            }
            var teacher = DbContext.Teachers
            .FirstOrDefault(sg => sg.Id == attachDetachSubjectToTeacherVm.TeacherId);
            if (teacher == null)
            {
                throw new ArgumentNullException($"The is no such attachment between group and subject");
            }
            DbContext.Teachers.Remove(teacher);
            DbContext.Remove(teacher);
            DbContext.SaveChanges();
            var subject = DbContext.Subjects.FirstOrDefault(x => x.Id == attachDetachSubjectToTeacherVm.SubjectId);
            var subjectVm = Mapper.Map<SubjectVm>(subject);
            return subjectVm;
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            if (filterPredicate == null)
            {
                throw new ArgumentNullException($"Predicate is null");
            }
            var groupEntity = DbContext.Groups
                .FirstOrDefault(filterPredicate);
            var groupVm = Mapper.Map<GroupVm>(groupEntity);
            return groupVm;
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null)
        {
            var groupEntities = DbContext.Groups.AsQueryable();
            if (filterPredicate != null)
                groupEntities = groupEntities.Where(filterPredicate);
            var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);
            return groupVms;
        }
    }
}