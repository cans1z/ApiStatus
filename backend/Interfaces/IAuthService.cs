using ApiStatus.DTOs.Auth;

namespace ApiStatus.Interfaces;

public interface IAuthService
{
    Task Register(RegisterDto dto);
    Task<string> Login(LoginDto dto);
}