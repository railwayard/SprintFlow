using SprintFlowApp.Application.DTOs.Projects;

namespace SprintFlowApp.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto> CreateAsync(CreateProjectDto dto);

    Task<List<ProjectDto>> GetAllAsync();

    Task<ProjectDto?> GetByIdAsync(Guid id);
}
