using Microsoft.EntityFrameworkCore;
using SprintFlowApp.Application.DTOs.Organizations;
using SprintFlowApp.Application.Interfaces;
using SprintFlowApp.Domain.Entities;
using SprintFlowApp.Infrastructure.Data;

namespace SprintFlowApp.Infrastructure.Services;

public class OrganizationService : IOrganizationService
{
    private readonly AppDbContext _dbContext;

    public OrganizationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto)
    {
        var organization = new Organization
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Organizations.Add(organization);
        await _dbContext.SaveChangesAsync();

        return new OrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            CreatedAt = organization.CreatedAt
        };
    }

    public async Task<List<OrganizationDto>> GetAllAsync()
    {
        return await _dbContext.Organizations
            .Select(o => new OrganizationDto
            {
                Id = o.Id,
                Name = o.Name,
                CreatedAt = o.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<OrganizationDto?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Organizations
            .Where(o => o.Id == id)
            .Select(o => new OrganizationDto
            {
                Id = o.Id,
                Name = o.Name,
                CreatedAt = o.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<OrganizationDto?> UpdateAsync(
        Guid id,
        UpdateOrganizationDto dto)
    {
        var organization = await _dbContext.Organizations
            .FirstOrDefaultAsync(o => o.Id == id);

        if (organization == null)
            return null;

        organization.Name = dto.Name;

        await _dbContext.SaveChangesAsync();

        return new OrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            CreatedAt = organization.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var organization = await _dbContext.Organizations
            .FirstOrDefaultAsync(o => o.Id == id);

        if (organization == null)
            return false;

        _dbContext.Organizations.Remove(organization);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}