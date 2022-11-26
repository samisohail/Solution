using AuthServices.Contracts;
using DataTransferObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthServices.Implementation
{
    public class JwtTokenHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;
        public JwtTokenHelper(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public string GenerateToken(UserDto user)
        {
            
            var expiry = _configuration["Jwt:Expiry"] ?? "7";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("id", user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(expiry)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // returns User Id from token claims
        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken valiatedToken);

                var jwtToken = (JwtSecurityToken)valiatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                return int.Parse(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}
