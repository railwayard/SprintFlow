namespace SprintFlowApp.Application.DTOs.Comments;

public class CommentDto
{
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public Guid TaskItemId { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}