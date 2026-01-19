using Domain.DTOs.Group;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class GroupController(IGroupService service) : ControllerBase
{
    [HttpGet("all")]
    public IActionResult GetAllGroup()
    {
        var res = service.GetAllGroups();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("{groupId}")]
    public IActionResult GetGroup(int groupId)
    {
        var res = service.GetGroup(groupId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("course_id")]
    public IActionResult GetCourse(int courseId)
    {
        var res = service.GetGroupByCourse(courseId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost]
    public IActionResult CreatedGroup(CreatedGroup dto)
    {
        var res = service.CreatedGroup(dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut]
    public IActionResult UpdateGroup(int id, UpdateGroup dto)
    {
        var res = service.UpdateGroup(id, dto);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete]
    public ActionResult DeleteGroup(int id)
    {
        var res = service.DeleteGroup(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("id/details")]
    public IActionResult GetGroupAndStudents()
    {
        var res = service.GetGroupAndStudents();
        return StatusCode(res.StatusCode, res);
    }
}