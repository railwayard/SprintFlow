using SprintFlowApp.Application.DTOs.Organizations;

namespace SprintFlowApp.Application.Interfaces;

public interface IOrganizationService
{
    Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto);

    Task<List<OrganizationDto>> GetAllAsync();

    Task<OrganizationDto?> GetByIdAsync(Guid id);

    Task<OrganizationDto?> UpdateAsync(Guid id, UpdateOrganizationDto dto);

    Task<bool> DeleteAsync(Guid id);
}