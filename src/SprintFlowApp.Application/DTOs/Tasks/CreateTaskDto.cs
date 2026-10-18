using System.ComponentModel.DataAnnotations;

namespace SprintFlowApp.Application.DTOs.Tasks;

public class CreateTaskDto
{
    [Required(ErrorMessage = "Task title is required.")]
    [StringLength(200, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid ProjectId { get; set; }

    [RegularExpression("^(Todo|InProgress|Done)$",
        ErrorMessage = "Status must be Todo, InProgress, or Done.")]
    public string Status { get; set; } = "Todo";

    [RegularExpression("^(Low|Medium|High|Critical)$",
        ErrorMessage = "Priority must be Low, Medium, High, or Critical.")]
    public string Priority { get; set; } = "Medium";
}
