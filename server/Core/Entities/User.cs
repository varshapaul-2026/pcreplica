using PcReplica.Core.Enums;

namespace PcReplica.Core.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public UserRole Role { get; set; }
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
    public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    public ICollection<Rfi> CreatedRfis { get; set; } = new List<Rfi>();
    public ICollection<Rfi> AssignedRfis { get; set; } = new List<Rfi>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<DailyLog> DailyLogs { get; set; } = new List<DailyLog>();
}
