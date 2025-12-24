using PcReplica.Core.Enums;

namespace PcReplica.Core.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public decimal? Budget { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();
    public ICollection<BudgetItem> BudgetItems { get; set; } = new List<BudgetItem>();
    public ICollection<Rfi> Rfis { get; set; } = new List<Rfi>();
    public ICollection<DailyLog> DailyLogs { get; set; } = new List<DailyLog>();
}
