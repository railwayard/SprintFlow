using SprintFlowApp.Domain.Common;

namespace SprintFlowApp.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public Guid RoleId { get; set; }

    public Role? Role { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}