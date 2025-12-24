namespace PcReplica.API.DTOs;

public class DocumentDto
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
    public long FileSize { get; set; }
    public string? ContentType { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Version { get; set; }
    public string? Description { get; set; }
    public int ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
}
