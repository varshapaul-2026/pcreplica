namespace PcReplica.Core.Entities;

public class DailyLog : BaseEntity
{
    public DateTime LogDate { get; set; }
    public string? WeatherConditions { get; set; }
    public decimal? Temperature { get; set; }
    public string? WorkCompleted { get; set; }
    public int? LaborHours { get; set; }
    public string? EquipmentUsed { get; set; }
    public string? SafetyIncidents { get; set; }
    public string? Notes { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
}
