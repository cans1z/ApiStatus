namespace ApiStatus.Models;

public class MonitorCheck
{
    public int Id { get; set; }
    public int MonitorId { get; set; }
    public int StatusCode { get; set; }
    public int ResponseTime { get; set; }
    public bool IsUp { get; set; }
    public DateTime CheckedAt { get; set; }
    
    public ServiceMonitor ServiceMonitor { get; set; } = null!;
}