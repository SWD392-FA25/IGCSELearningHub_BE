using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Devices
{
    public class DeviceUnregisterRequest
    {
        [Required]
        public string DeviceToken { get; set; } = string.Empty;
    }
}
