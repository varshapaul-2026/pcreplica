namespace PcReplica.Core.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int? RfiId { get; set; }
    public Rfi? Rfi { get; set; }
    public int? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
