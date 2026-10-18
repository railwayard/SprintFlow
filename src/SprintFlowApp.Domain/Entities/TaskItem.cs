using SprintFlowApp.Domain.Common;

namespace SprintFlowApp.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }

    public string Status { get; set; } = "Todo";
    public string Priority { get; set; } = "Medium";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}