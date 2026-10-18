namespace SprintFlowApp.Application.DTOs.Organizations;

public class OrganizationDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}