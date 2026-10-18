using System.ComponentModel.DataAnnotations;

namespace SprintFlowApp.Application.DTOs.Organizations;

public class CreateOrganizationDto
{
    [Required(ErrorMessage = "Organization name is required.")]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
}
