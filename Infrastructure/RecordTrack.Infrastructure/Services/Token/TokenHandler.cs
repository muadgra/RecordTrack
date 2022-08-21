using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecordTrack.Application.Abstractions.Token;
using RecordTrack.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Application.Abstractions.DTOs.Token CreateAccessToken(int minutes, AppUser user)
        {
            Application.Abstractions.DTOs.Token token = new();
            
            //security key
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //şifrelenmiş kimliği oluştur
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //token ayarları ver
            token.Expiration = DateTime.UtcNow.AddMinutes(minutes);
            JwtSecurityToken securityToken = new(
                    audience: _configuration["Token:Audience"],
                    issuer: _configuration["Token:Issuer"],
                    expires: token.Expiration,
                    notBefore: DateTime.UtcNow, //üretildiği anda devreye girer
                    signingCredentials: signingCredentials,
                    claims: new List<Claim> { new(ClaimTypes.Name, user.UserName)}
                    
                    );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken(25);
            return token;

            
        }

        public string CreateRefreshToken(int minutes)
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
