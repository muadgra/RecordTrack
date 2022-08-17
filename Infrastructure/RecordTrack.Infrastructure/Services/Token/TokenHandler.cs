using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecordTrack.Application.Abstractions.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        public Application.Abstractions.DTOs.Token CreateAccessToken(int minutes)
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
                    signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;

            
        }
    }
}
