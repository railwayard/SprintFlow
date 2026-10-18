using Microsoft.AspNetCore.Mvc;
using SprintFlowApp.Application.DTOs.Projects;
using SprintFlowApp.Application.Interfaces;

namespace SprintFlowApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(
        CreateProjectDto dto)
    {
        var project = await _projectService.CreateAsync(dto);

        return Ok(project);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetAll()
    {
        var projects = await _projectService.GetAllAsync();

        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectDto>> GetById(Guid id)
    {
        var project = await _projectService.GetByIdAsync(id);

        if (project is null)
            return NotFound();

        return Ok(project);
    }
}
