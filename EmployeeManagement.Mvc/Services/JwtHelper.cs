using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagement.Mvc.Services
{
    public class JwtHelper
    {
        public static Dictionary<string, string> DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);
        }
    }
}
