namespace ApiStatus.DTOs.Monitor;

public class MonitorResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int IntervalSeconds { get; set; }
    
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}