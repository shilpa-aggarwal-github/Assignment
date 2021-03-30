using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Features.Account.Model
{
    public class LoginResponseModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public bool IsDiscount { get; set; }
        public Guid TenantId { get; set; }
        
    }
}
