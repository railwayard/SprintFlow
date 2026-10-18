using SprintFlowApp.Domain.Common;

namespace SprintFlowApp.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = string.Empty;

    public Guid TaskItemId { get; set; }
    public Guid UserId { get; set; }

    public TaskItem TaskItem { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
