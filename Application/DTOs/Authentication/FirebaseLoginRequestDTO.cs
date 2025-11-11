using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Authentication
{
    public class FirebaseLoginRequestDTO
    {
        [Required]
        public string FirebaseIdToken { get; set; } = string.Empty;
    }
}
