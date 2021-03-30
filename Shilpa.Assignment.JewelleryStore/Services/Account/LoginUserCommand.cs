using MediatR;
using Shilpa.Assignment.JewelleryStore.Features.Account;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using Shilpa.Assignment.JewelleryStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Services.Account
{
    public class LoginUserCommand:IRequest<SuccessResponseDTO<LoginResponseModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string TenantName{ get; set; }

        public string SceretKey { get; set; }
        public IJWTAuth  JWTAuth { get;set; }
    }
}
