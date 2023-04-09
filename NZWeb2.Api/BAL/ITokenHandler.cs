using Microsoft.IdentityModel.Tokens;
using NZWeb2.Api.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWeb2.Api.BAL
{
    //Step - 8
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _IConfiguration;
        public TokenHandler(IConfiguration Configuration) {
            _IConfiguration = Configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            //Create Clamims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));

            //Loop into roles
            user.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Jwt:Key"]));

            //create Token
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _IConfiguration["Jwt:Issuer"], 
                _IConfiguration["Jwt:Audience"], 
                claims, 
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
    //
}
