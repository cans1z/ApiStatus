using ApiStatus.Data;
using ApiStatus.DTOs.Auth;
using ApiStatus.Exceptions;
using ApiStatus.Interfaces;
using ApiStatus.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStatus.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    public async Task Register(RegisterDto dto)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower());

        if (userExists)
        {
            throw new ConflictException($"User with email {dto.Email} already exists");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Email = dto.Email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };
        
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        
    }

    public async Task<string> Login(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid credentials");
        
        return _tokenService.GenerateToken(user);
    }
}