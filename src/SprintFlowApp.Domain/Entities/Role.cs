using SprintFlowApp.Domain.Common;

namespace SprintFlowApp.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}