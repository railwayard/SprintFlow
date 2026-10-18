using SprintFlowApp.Application.DTOs.Tasks;

namespace SprintFlowApp.Application.Interfaces;

public interface ITaskService
{
    Task<TaskDto> CreateAsync(CreateTaskDto dto);

    Task<List<TaskDto>> GetAllAsync();

    Task<TaskDto?> GetByIdAsync(Guid id);
}