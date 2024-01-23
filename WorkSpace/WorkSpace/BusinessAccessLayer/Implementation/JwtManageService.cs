using BusinessAccessLayer.Abstraction;
using Common.Constants;
using Entities.DataModels;
using Entities.DTOs.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Implementation
{
    public class JwtManageService : IJwtManageService
    {
        #region Properties

        public IConfiguration _configuration;
        public IHttpContextAccessor _httpContext;

        #endregion


        #region Cunstructor

        public JwtManageService(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContext = httpContext;
        }

        #endregion


        #region Methods

        public TokensDto GenerateToken(User user)
        {
            return GenerateJwtToken(user);
        }

        public TokensDto GenerateRefreshToken(User user)
        {
            return GenerateJwtToken(user);
        }

        public TokensDto GenerateJwtToken(User user)
        {
            //set key and credential
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //add claim
            var claims = new[]
            {
                new Claim(SystemConstants.UserIdClaim,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.FirstName+" "+user.LastName)
            };

            //make token
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials);
            return new TokensDto { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = GenerateRefreshToken() };
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        #endregion
    }
}
