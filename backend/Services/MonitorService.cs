using ApiStatus.Data;
using ApiStatus.DTOs.Monitor;
using ApiStatus.Exceptions;
using ApiStatus.Interfaces;
using ApiStatus.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStatus.Services;

public class MonitorService : IMonitorService
{
    private readonly AppDbContext _db;

    public MonitorService(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<MonitorResponseDto>> GetAllMonitors(int userId)
    {
        var monitors = await _db.Monitors
            .Where(m => m.UserId == userId)
            .Select(m => new MonitorResponseDto
            {
                Id = m.Id,
                UserId = m.UserId,
                Name = m.Name,
                Url = m.Url,
                IntervalSeconds = m.Interval,
                IsActive = m.IsActive,
                CreatedAt = m.CreatedAt,
            })
            .ToListAsync();
        
        return monitors;
    }

    public async Task<MonitorResponseDto> GetMonitorById(int monitorId, int userId)
    {
        var monitor = await _db.Monitors
            .FirstOrDefaultAsync(m => m.Id == monitorId && m.UserId == userId);
        
        if (monitor == null)
            throw new NotFoundException("Monitor not found");
        
        var response = new MonitorResponseDto()
        {
            Id = monitor.Id,
            UserId = monitor.UserId,
            Name = monitor.Name,
            Url = monitor.Url,
            IntervalSeconds = monitor.Interval,
            IsActive = monitor.IsActive,
            CreatedAt = monitor.CreatedAt
        };
        
        return response;
    }

    public async Task<MonitorResponseDto> CreateMonitor(int userId, AddMonitorDto dto)
    {
        var monitorExists = await _db.Monitors
            .AnyAsync(m => m.Url == dto.Url && m.UserId == userId);

        if (monitorExists)
        {
            throw new ConflictException($"Monitor with Url {dto.Url} already exists");
        }

        var newMonitor = new ServiceMonitor()
        {
            Name = dto.Name,
            Url = dto.Url,
            Interval = dto.IntervalSeconds,
            UserId = userId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        
        await _db.Monitors.AddAsync(newMonitor);
        await _db.SaveChangesAsync();

        var response = new MonitorResponseDto
        {
            Id = newMonitor.Id,
            UserId = newMonitor.UserId,
            Name = dto.Name,
            Url = dto.Url,
            IntervalSeconds = dto.IntervalSeconds,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        
        return response;
    }

    public async Task DeleteMonitorById(int userId, int id)
    {
        var monitor = await _db.Monitors
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
        
        if (monitor == null)
            throw new NotFoundException($"Monitor with id {id} does not exist");
        
        _db.Monitors.Remove(monitor);
        await _db.SaveChangesAsync();
    }
}