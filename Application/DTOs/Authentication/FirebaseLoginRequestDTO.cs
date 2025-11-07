using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Authentication
{
    public class FirebaseLoginRequestDTO
    {
        [Required]
        public string IdToken { get; set; } = string.Empty;
    }
}
