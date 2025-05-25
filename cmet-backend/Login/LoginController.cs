using cmet_backend.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cmet_backend.Login
{
    [Route("v1/login")]
    public class LoginController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> Get(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "admin") // добавляем роль
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nV59vHgN9lwniX1+c2zghSKcKpz7HhTrb8F1WogjD4E="));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return ApiOk(tokenString);
        }
    }
}
