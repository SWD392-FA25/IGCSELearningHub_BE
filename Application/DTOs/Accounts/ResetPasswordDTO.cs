using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Accounts
{
    public class ResetPasswordDTO
    {
        [Required]
        public string UserNameOrEmail { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
