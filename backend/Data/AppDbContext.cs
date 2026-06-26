using ApiStatus.Models;
using Microsoft.EntityFrameworkCore;
using Monitor = System.Threading.Monitor;

namespace ApiStatus.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<ServiceMonitor> Monitors => Set<ServiceMonitor>();
    public DbSet<MonitorCheck> MonitorChecks => Set<MonitorCheck>();
}