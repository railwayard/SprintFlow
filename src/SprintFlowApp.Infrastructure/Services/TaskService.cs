using Microsoft.EntityFrameworkCore;
using SprintFlowApp.Application.DTOs.Tasks;
using SprintFlowApp.Application.Interfaces;
using SprintFlowApp.Domain.Entities;
using SprintFlowApp.Infrastructure.Data;

namespace SprintFlowApp.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _dbContext;

    public TaskService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            ProjectId = dto.ProjectId,
            Status = dto.Status,
            Priority = dto.Priority,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.TaskItems.Add(task);

        await _dbContext.SaveChangesAsync();

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId,
            Status = task.Status,
            Priority = task.Priority,
            CreatedAt = task.CreatedAt
        };
    }

    public async Task<List<TaskDto>> GetAllAsync()
    {
        return await _dbContext.TaskItems
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ProjectId = t.ProjectId,
                Status = t.Status,
                Priority = t.Priority,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<TaskDto?> GetByIdAsync(Guid id)
    {
        return await _dbContext.TaskItems
            .Where(t => t.Id == id)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ProjectId = t.ProjectId,
                Status = t.Status,
                Priority = t.Priority,
                CreatedAt = t.CreatedAt
            })
            .FirstOrDefaultAsync();
    }
}