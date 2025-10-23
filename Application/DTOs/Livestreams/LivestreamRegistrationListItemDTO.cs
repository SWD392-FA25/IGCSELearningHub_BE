using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Livestreams
{
    public class LivestreamRegistrationListItemDTO
    {
        public int RegistrationId { get; set; }
        public int AccountId { get; set; }
        public string? AccountUserName { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public DateTime RegisteredAt { get; set; }
    }
}
