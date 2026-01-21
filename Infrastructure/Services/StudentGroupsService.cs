using System.Net;
using Domain.DTOs.Group;
using Domain.DTOs.Student;
using Domain.DTOs.StudentGroups;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentGroupsService(ApplicationDbContext context) : IStudentGroupsService
{
    #region AddStudentInGroup

    public Response<string> AddStudentInGroup(AddStudentGroups dto)
    {
        try
        {
            var add = new StudentGroups()
            {
                StudentId = dto.StudentId,
                GroupId = dto.GroupId,
            };

            context.StudentGroups.Add(add);
            var res = context.SaveChanges();

            return res > 0
                ? new Response<string>(HttpStatusCode.Created, "Add Student in Group")
                : new Response<string>(HttpStatusCode.BadRequest, "Bad Request");
        }
        catch
        {
            return new Response<string>(HttpStatusCode.InternalServerError, "Error");
        }
    }

    #endregion

    #region RemoveStudent

    public Response<string> RemoveStudent(int id)
    {
        try
        {
            var res = context.StudentGroups.Where(x => x.StudentId == id).ToList();
            if (res.Count == 0)
                return new Response<string>(HttpStatusCode.NotFound, "Not Found");
            
            context.StudentGroups.RemoveRange(res);
            context.SaveChanges();

            return new Response<string>(HttpStatusCode.OK, "Removed Student Group");
        }
        catch
        {
            return new Response<string>(HttpStatusCode.InternalServerError, "Error");
        }
    }

    #endregion

    #region GetAllStudents

    public Response<List<GetStudentGroups>> GetAllStudents(int studentId)
    {
        var res = context.StudentGroups.Include(x => x.Students).Include(x => x.Groups).Where(x => x.StudentId == studentId).ToList();
        if (res.Count == 0)
            return new Response<List<GetStudentGroups>>(HttpStatusCode.NotFound, "count = 0");
        var getAll = res.Select(x => new GetStudentGroups()
            {
                Id = x.Id,
                
                Groups = new GetGroup
                {
                    Id = x.Groups!.Id,
                    Name = x.Groups.Name,
                    CourseId = x.Groups.CourseId,
                    IsStarted = x.Groups.IsStarted,
                    StartDate = x.Groups.StartDate,
                    EndDate = x.Groups.EndDate,
                }
            }
        ).ToList();
        
        return new Response<List<GetStudentGroups>>(HttpStatusCode.OK, "All Student Groups");
    }

    #endregion

    #region StudentCount

    public Response<int> StudentCount()
    {
        var res = context.StudentGroups.Where(x => !x.IsDeleted).Select(x => x.StudentId).Distinct().Count();
        return new Response<int>(res);
    }

    #endregion
}