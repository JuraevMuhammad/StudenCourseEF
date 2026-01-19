using System.Net;
using Domain.DTOs.Course;
using Domain.DTOs.Group;
using Domain.DTOs.Student;
using Domain.DTOs.StudentGroups;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GroupService(ApplicationDbContext context) : IGroupService
{
    #region GetAllGroups

    public Response<List<GetGroup>> GetAllGroups()
    {
        var res = context.Groups.Where(x => x.IsDeleted == false).ToList();
        var getGroups = res.Select(x => new GetGroup()
            {
                Id = x.Id,
                Name = x.Name,
                CourseId = x.CourseId,
                IsStarted = x.IsStarted,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
            }
        ).ToList();
        
        return new Response<List<GetGroup>>(getGroups);
    }

    #endregion

    #region GetGroup

    public Response<GetGroup> GetGroup(int groupId)
    {
        var res = context.Groups.FirstOrDefault(x => x.Id == groupId && x.IsDeleted == false);
        if (res == null)
        {
            return new Response<GetGroup>(HttpStatusCode.NotFound, "not found");
        }

        var group = new GetGroup()
        {
            Id = res.Id,
            Name = res.Name,
            CourseId = res.CourseId,
            IsStarted = res.IsStarted,
            StartDate = res.StartDate,
            EndDate = res.EndDate,
        };
        return new Response<GetGroup>(group);
    }

    #endregion

    #region GetGroupByCourseId

    public Response<List<GetGroup>> GetGroupByCourse(int courseId)
    {
        var res = context.Groups.Include(x => x.Course).Where(x => x.CourseId == courseId && x.IsDeleted == false).Select(x => new GetGroup()
            {
                Id = x.Id,
                Name = x.Name,
                CourseId = x.CourseId,
                IsStarted = x.IsStarted,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Course = new GetCourse()
                {
                    Id = x.Course!.Id,
                    Title = x.Course!.Title,
                    Description = x.Course!.Description,
                    Price = x.Course.Price,
                    TeacherId = x.Course.TeacherId,
                    CreatedAt = x.CreatedAt,
                }
            }
        ).ToList();
        
        return res.Count == 0
            ? new Response<List<GetGroup>>(HttpStatusCode.NotFound, "not found")
            : new Response<List<GetGroup>>(res);
    }

    #endregion

    #region CreatedGroup

    public Response<string> CreatedGroup(CreatedGroup dto)
    {
        var newGroup = new Domain.Entities.Group()
        {
            Name = dto.Name,
            CourseId = dto.CourseId,
            IsStarted = dto.IsStarted,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
        };
        context.Groups.Add(newGroup);
        var res = context.SaveChanges();
        return new Response<string>(HttpStatusCode.Created, "Created Group");
    }

    #endregion

    #region UpdateGroup

    public Response<string> UpdateGroup(int id, UpdateGroup dto)
    {
        var res = context.Groups.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        res.Name = dto.Name ?? res.Name;
        res.CourseId = dto.CourseId ?? res.CourseId;
        res.IsStarted = dto.IsStarted ?? res.IsStarted;
        res.StartDate = dto.StartDate ?? res.StartDate;
        res.EndDate = dto.EndDate ?? res.EndDate;
        var result = context.SaveChanges();
        return new Response<string>(HttpStatusCode.Accepted, "Group Updated");
    }

    #endregion

    #region DeleteGroup

    public Response<string> DeleteGroup(int groupId)
    {
        var res = context.Groups.FirstOrDefault(x => x.Id == groupId && x.IsDeleted == false);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        res.IsDeleted = true;
        var result = context.SaveChanges();
        return new Response<string>(HttpStatusCode.OK, "Group Deleted");
    }

    #endregion

    #region StartLesson

    public Response<string> StartLesson(int groupId)
    {
        var res = context.Groups.FirstOrDefault(x => x.Id == groupId && x.IsStarted == false);
        if (res == null)
            return new Response<string>(HttpStatusCode.NotFound, "not found");
        
        if (res.StartDate <= DateTime.UtcNow && res.EndDate >= DateTime.UtcNow)
            res.IsStarted = true;
        
        var result = context.SaveChanges();
        return new Response<string>(HttpStatusCode.OK, "Group Started");
    }

    #endregion

    #region GetGroupAndStudents

    public Response<List<GetGroup>> GetGroupAndStudents()
    {
        var get = context.Groups.Include(x => x.StudentGroups!).ThenInclude(x => x.Students).Where(x => x.IsDeleted == false).ToList();

        var res = get.Select(x => new GetGroup()
            {
                Id = x.Id,
                Name = x.Name,
                CourseId = x.CourseId,
                StartDate = x.StartDate,
                IsStarted = x.IsStarted,
                EndDate = x.EndDate,
                isDeleted = x.IsDeleted,
                StudentGroups = x.StudentGroups!.Select(m => new GetStudent()
                    {
                        Id = m.Students!.Id,
                        FirstName = m.Students.FirstName,
                        LastName = m.Students.LastName,
                        Email = m.Students.Email,
                        Phone = m.Students.Phone,
                        BirthDate = m.Students.BirthDate
                    }
                ).ToList()
            }
        ).ToList();
        
        return new Response<List<GetGroup>>(res);
    }

    #endregion
}