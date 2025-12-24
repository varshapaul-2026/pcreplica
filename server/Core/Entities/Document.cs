using PcReplica.Core.Enums;

namespace PcReplica.Core.Entities;

public class Document : BaseEntity
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
    public long FileSize { get; set; }
    public string? ContentType { get; set; }
    public DocumentCategory Category { get; set; }
    public int Version { get; set; } = 1;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public int UploadedByUserId { get; set; }
    public string? Description { get; set; }
}
