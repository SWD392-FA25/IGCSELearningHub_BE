using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Accounts
{
    public class ForgotPasswordRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
