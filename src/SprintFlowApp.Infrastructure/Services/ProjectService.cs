using Microsoft.EntityFrameworkCore;
using SprintFlowApp.Application.DTOs.Projects;
using SprintFlowApp.Application.Interfaces;
using SprintFlowApp.Domain.Entities;
using SprintFlowApp.Infrastructure.Data;

namespace SprintFlowApp.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _dbContext;

    public ProjectService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            OrganizationId = dto.OrganizationId,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Projects.Add(project);

        await _dbContext.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            OrganizationId = project.OrganizationId,
            CreatedAt = project.CreatedAt
        };
    }

    public async Task<List<ProjectDto>> GetAllAsync()
    {
        return await _dbContext.Projects
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OrganizationId = p.OrganizationId,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Projects
            .Where(p => p.Id == id)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OrganizationId = p.OrganizationId,
                CreatedAt = p.CreatedAt
            })
            .FirstOrDefaultAsync();
    }
}