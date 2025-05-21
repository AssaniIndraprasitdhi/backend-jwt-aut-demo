using backend_jwt_auth.Models;

namespace backend_jwt_auth.Services;

public interface IJwtService {
    string GenerateToken(User user);
}