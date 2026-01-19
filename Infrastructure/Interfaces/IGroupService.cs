using Domain.DTOs.Group;
using Domain.DTOs.StudentGroups;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IGroupService
{
    Response<List<GetGroup>> GetAllGroups();
    Response<GetGroup> GetGroup(int groupId);
    Response<List<GetGroup>> GetGroupByCourse(int courseId);
    Response<string> CreatedGroup (CreatedGroup dto);
    Response<string> UpdateGroup (int id, UpdateGroup dto);
    Response<string> DeleteGroup(int groupId);
    Response<string> StartLesson(int groupId);
    Response<List<GetGroup>> GetGroupAndStudents();
}