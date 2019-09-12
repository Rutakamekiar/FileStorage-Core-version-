using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FileStorage.WebApi
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "http://localhost:51884/";
        public const int Lifetime = 60 * 24 * 7;
        public static string Key { get; } = "secretKey!123";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}