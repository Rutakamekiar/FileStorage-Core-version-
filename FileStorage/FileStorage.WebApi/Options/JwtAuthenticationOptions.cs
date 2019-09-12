using System;
using Microsoft.IdentityModel.Tokens;

namespace FileStorage.WebApi.Options
{
    public class JwtAuthenticationOptions
    {
        public string SecurityKey { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(SecurityKey));

        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}
