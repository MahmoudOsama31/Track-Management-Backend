using Application.DTOs.Auth;

namespace Application.Interfaces.Services;

public interface ITokenService
{
    TokenResponseDto GenerateToken(string username);
}
