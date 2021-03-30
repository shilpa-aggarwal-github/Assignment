using Microsoft.IdentityModel.Tokens;
using Moq;
using Shilpa.Assignment.JewelleryStore.Features.Account;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Shilpa.Assignment.JewelleryStore.Tests.Unit
{
    public  class MockJwtTokens:IJWTAuth
    {
        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();
        private static readonly RandomNumberGenerator s_rng = RandomNumberGenerator.Create();
        private static readonly byte[] s_key = new byte[32];

        static MockJwtTokens()
        {
            s_rng.GetBytes(s_key);
            SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
            SigningCredentials = new SigningCredentials(new RsaSecurityKey(new RSACryptoServiceProvider(2048).ExportParameters(true)), SecurityAlgorithms.RsaSha256Signature);
        }


        public string GenerateToken(LoginResponseModel user, DateTime expires)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim("ID", user.UserId.ToString()),
                new Claim("Role",user.Role.ToString())
            };

            var identityMock = new Mock<ClaimsIdentity>();
            identityMock.Setup(x => x.Claims).Returns(claims);

            return s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, expires, SigningCredentials));
        }
    }
}
