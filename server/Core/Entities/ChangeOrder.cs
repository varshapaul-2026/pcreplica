namespace PcReplica.Core.Entities;

public class ChangeOrder : BaseEntity
{
    public string Number { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public int BudgetItemId { get; set; }
    public BudgetItem BudgetItem { get; set; } = null!;
}
