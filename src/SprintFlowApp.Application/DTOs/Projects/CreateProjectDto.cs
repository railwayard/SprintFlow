using System.ComponentModel.DataAnnotations;

namespace SprintFlowApp.Application.DTOs.Projects;

public class CreateProjectDto
{
    [Required(ErrorMessage = "Project name is required.")]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid OrganizationId { get; set; }
}
