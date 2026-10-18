using Microsoft.AspNetCore.Mvc;
using SprintFlowApp.Application.DTOs.Tasks;
using SprintFlowApp.Application.Interfaces;

namespace SprintFlowApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create(
        CreateTaskDto dto)
    {
        var task = await _taskService.CreateAsync(dto);

        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();

        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskDto>> GetById(Guid id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }
}
