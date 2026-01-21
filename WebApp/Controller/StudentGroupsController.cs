using Domain.DTOs.StudentGroups;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]/student-groups")]
public class StudentGroupsController(IStudentGroupsService service) : ControllerBase
{
    [HttpPost]
    public IActionResult AddStudentGroup(AddStudentGroups dto)
    {
        var res = service.AddStudentInGroup(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete]
    public IActionResult RemoveStudents(int id)
    {
        var res = service.RemoveStudent(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet]
    public IActionResult GetStudents(int id)
    {
        var res = service.GetAllStudents(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("count-student-is-active")]
    public IActionResult GetStudentsIsActive()
    {
        var res = service.StudentCount();
        return StatusCode(res.StatusCode, res);
    }
}