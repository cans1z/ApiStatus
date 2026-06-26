using ApiStatus.Models;

namespace ApiStatus.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}