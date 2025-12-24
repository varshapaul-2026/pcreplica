namespace PcReplica.Core.Entities;

public class Task : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public int? AssignedToUserId { get; set; }
    public int? MilestoneId { get; set; }
    public Milestone? Milestone { get; set; }
}
