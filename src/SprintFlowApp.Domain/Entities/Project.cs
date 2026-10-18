using SprintFlowApp.Domain.Common;

namespace SprintFlowApp.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid OrganizationId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Organization Organization { get; set; } = null!;

    public ICollection<TaskItem> Tasks { get; set; }
        = new List<TaskItem>();
}