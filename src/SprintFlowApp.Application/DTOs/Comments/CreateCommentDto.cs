using System.ComponentModel.DataAnnotations;

namespace SprintFlowApp.Application.DTOs.Comments;

public class CreateCommentDto
{
    [Required(ErrorMessage = "Comment content is required.")]
    [StringLength(2000, MinimumLength = 1)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public Guid TaskItemId { get; set; }

    [Required]
    public Guid UserId { get; set; }
}
