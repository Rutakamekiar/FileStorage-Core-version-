using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer"; // издатель токена
        public const string Audience = "http://localhost:51884/"; // потребитель токена
        public static string Key { get; } = "mysupersecret_secretkey!123";
        public const int Lifetime = 60 * 24 * 7; // время жизни токена - 1 минута

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}