using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _taskService.GetAll(UserId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _taskService.GetById(id, UserId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskDto([FromBody] TaskDto dto)
    {
        await _taskService.Create(dto, UserId);
        return Ok("Created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskDto(string id, TaskDto dto)
    {
        await _taskService.Update(id, dto, UserId);
        return Ok("Updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _taskService.Delete(id, UserId);
        return Ok("Deleted");
    }
}
