namespace PcReplica.Core.Entities;

public class BudgetItem : BaseEntity
{
    public string CostCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BudgetedAmount { get; set; }
    public decimal ActualAmount { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public ICollection<ChangeOrder> ChangeOrders { get; set; } = new List<ChangeOrder>();
}
