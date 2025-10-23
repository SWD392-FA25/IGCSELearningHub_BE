using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Livestreams
{
    public class UpdateRegistrationPaymentStatusDTO
    {
        public string PaymentStatus { get; set; } = null!; // ví dụ: Pending|Succeeded|Failed
    }
}
