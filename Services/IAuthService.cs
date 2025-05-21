using backend_jwt_auth.Models.DTOs;
using System.Threading.Tasks;

namespace backend_jwt_auth.Services;

public interface IAuthService {
    Task<bool> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}
