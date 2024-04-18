using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System_Zarzadzania_Lotami.Data.Entities;
using System_Zarzadzania_Lotami.Helper;
using System_Zarzadzania_Lotami.Services;

namespace System_Zarzadzania_Lotami.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings) : ControllerBase
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IAuthService _authService = authService;

        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] User user)
        {
            if (!await _authService.ValidateCredentials(user.Username, user.Password))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var token = GenerateJwtToken(user.Username);
            return Ok(new { token });
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
