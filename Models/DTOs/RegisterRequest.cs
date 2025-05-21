namespace backend_jwt_auth.Models.DTOs;

public class RegisterRequest
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
}
