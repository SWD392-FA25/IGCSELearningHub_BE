using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Submissions
{
    public class CreateSubmissionRequest
    {
        public string AttachmentUrl { get; set; } = null!;
        public string? TextAnswer { get; set; }
    }
}
