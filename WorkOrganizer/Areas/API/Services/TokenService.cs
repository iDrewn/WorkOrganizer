using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WorkOrganizer.Areas.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> userManager;   //usermanager använder dbcontext

        private readonly IConfiguration configuration;

        public TokenService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;

            this.configuration = configuration;
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);         

            if (user == null)
            {
                return null;
            }

            var isValidUser = await userManager.CheckPasswordAsync(user, password);

            if(!isValidUser)
            {
                return null;
            }
            // för att generera en riktig JSON Web Token. configuration nedan hänvisar till appsettings-filen 
            var signingKey = Convert.FromBase64String(configuration["jwt:SigningSecret"]);
            var expiryDurationInMinutes = int.Parse(configuration["jwt:ExpiryDurationInMinutes"]);


        //skapar en SecurityTokenDescriptor. Denna håller all information relaterad till en security token:
            var tokenDescriptor = new SecurityTokenDescriptor   //använder ".tokens "       //beskriver vår token 
            {
                Issuer = null,         //vem som utfärdar token, dvs vi själva          //iss           //sätter claims
                Audience = null,        //aud

                IssuedAt = DateTime.UtcNow,     //iat       //när vi utfärdar JWT
                NotBefore = DateTime.UtcNow,    //nbf       //när JWT börjar gälla. samma tid som ovan. 
                Expires = DateTime.UtcNow.AddMinutes(expiryDurationInMinutes),      //exp           //giltighetstid
                Subject = new ClaimsIdentity(new List<Claim>  //sub          // Identifierar för vem JWT-token gäller.
                {
                    new Claim("userid", user.Id.ToString()),                    //publika claims 
                    new Claim("email", user.Email)                      //lägger till Claims
                }),
                SigningCredentials = new SigningCredentials(new                 //signerings-hantering
                    SymmetricSecurityKey(signingKey),
                    SecurityAlgorithms.HmacSha256Signature)

            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();                    //dessa tre rader kod behöver vi inte lära oss alls
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);           //token som vi vill returnera 
            
            return await Task.FromResult(token);

        }
    }
}
