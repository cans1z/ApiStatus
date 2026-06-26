namespace ApiStatus.Models;

public class ServiceMonitor
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Interval { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
    public ICollection<MonitorCheck> Checks { get; set; } = new List<MonitorCheck>();
}