using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
    public class AuthenticatedUserDTO
    {
        public string JWTToken { get; set; } = string.Empty;
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public bool IsExternal { get; set; }
    }
}
