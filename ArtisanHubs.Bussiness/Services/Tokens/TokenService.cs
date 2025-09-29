//using ArtisanHubs.Data.Entities;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace ArtisanHubs.Bussiness.Services.Tokens
//{
//    public class TokenService : ITokenService
//    {
//        private readonly IConfiguration _configuration;
//        private readonly SymmetricSecurityKey _jwtKey;

//        public TokenService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//        }

//        public string CreateToken(Account account)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
//                new Claim(ClaimTypes.Email, account.Email),
//                new Claim(ClaimTypes.Role, account.Role)
//                //new Claim("email", account.Email),
//                //new Claim("role", account.Role)
//            };

//            var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);
//            //var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256Signature);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = DateTime.UtcNow.AddDays(7), // Token hết hạn sau 7 ngày
//                SigningCredentials = credentials,
//                Issuer = _configuration["Jwt:Issuer"],
//                Audience = _configuration["Jwt:Audience"]
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var token = tokenHandler.CreateToken(tokenDescriptor);

//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
using ArtisanHubs.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtisanHubs.Bussiness.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _jwtKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(IConfiguration configuration)
        {
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }

        public string CreateToken(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                
                // SỬA LỖI 2: Dùng chuỗi "email" và "role" để khớp với Program.cs
                new Claim(ClaimTypes.Email, account.Email), // <-- Sửa lại
        new Claim(ClaimTypes.Role, account.Role)     // <-- Sửa lại
            };

            // SỬA LỖI 1: Dùng thuật toán HmacSha256Signature
            var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _issuer,
                Audience = _audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}