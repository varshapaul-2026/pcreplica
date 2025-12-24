using PcReplica.Core.Enums;

namespace PcReplica.Core.Entities;

public class Rfi : BaseEntity
{
    public string Number { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public string? Answer { get; set; }
    public RfiStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? AnsweredAt { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    public int? AssignedToUserId { get; set; }
    public User? AssignedToUser { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
