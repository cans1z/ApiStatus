using System.Security.Claims;
using ApiStatus.DTOs.Monitor;
using ApiStatus.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiStatus.Controllers;

[ApiController]
[Authorize]
[Route("api/monitor")]
public class MonitorController : ControllerBase
{
    private IMonitorService _monitorService;

    public MonitorController(IMonitorService monitorService)
    {
        _monitorService = monitorService;
    }
    
    [HttpGet]
    public async Task<ActionResult<MonitorResponseDto>> GetAllMonitors()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _monitorService.GetAllMonitors(userId);
        
        return Ok(user);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<MonitorResponseDto>> GetMonitorById(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _monitorService.GetMonitorById(id, userId);
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<MonitorResponseDto>> CreateMonitor(AddMonitorDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var monitor = await _monitorService.CreateMonitor(userId, dto);
        
        return Ok(monitor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMonitor(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _monitorService.DeleteMonitorById(userId, id);
        
        return NoContent();
    }
}