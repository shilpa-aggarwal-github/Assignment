using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Features.Account
{
    public interface IJWTAuth
    {
        public string GenerateToken(LoginResponseModel user, DateTime expires);
    }
}
