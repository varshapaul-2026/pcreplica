namespace PcReplica.Core.Entities;

public class Milestone : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
