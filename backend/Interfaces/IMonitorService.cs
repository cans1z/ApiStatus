using ApiStatus.DTOs.Monitor;
using ApiStatus.Models;

namespace ApiStatus.Interfaces;

public interface IMonitorService
{
    Task<List<MonitorResponseDto>> GetAllMonitors(int userId);
    Task<MonitorResponseDto> GetMonitorById(int id, int userId);
    Task<MonitorResponseDto> CreateMonitor(int userId, AddMonitorDto dto);
    Task DeleteMonitorById(int userId, int id);
}