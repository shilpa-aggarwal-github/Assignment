using Microsoft.IdentityModel.Tokens;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Shilpa.Assignment.JewelleryStore.Features.Account
{
    public class JWTAuth : IJWTAuth
    {
        public string GenerateToken(LoginResponseModel user, DateTime expires)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Email, "TokenAuth"),
                    new[] {
                new Claim("ID", user.UserId.ToString()),
                new Claim("Role",user.Role.ToString())
                    }
                );

                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {

                    Issuer = "Issuer",
                    Audience = "Audience",
                    SigningCredentials = new SigningCredentials(new RsaSecurityKey(new RSACryptoServiceProvider(2048).ExportParameters(true)), SecurityAlgorithms.RsaSha256Signature),
                    Subject = identity,
                    Expires = expires,
                    NotBefore = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30))
                });
                return handler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
