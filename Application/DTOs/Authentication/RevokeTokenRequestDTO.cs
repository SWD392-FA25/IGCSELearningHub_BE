namespace Application.DTOs.Authentication
{
    public class RevokeTokenRequestDTO
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }
}

