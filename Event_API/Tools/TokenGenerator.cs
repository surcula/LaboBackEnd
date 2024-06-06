using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Event_API_Domain.Models;
using Event_API_DAL.Models;
namespace Event_API.Tools
{
    public class TokenGenerator
    {
        private readonly string _secretkey;
        public TokenGenerator(IConfiguration config)
        {
            _secretkey = config.GetSection("TokenInfo").GetSection("secretKey").Value;
        }

        public string GenerateToken(Persons persons)
        {
            //Generation de la Verify Signature
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretkey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Generation du Payload (Data)
            Claim[] myClaims = new[]
            {
                new Claim(ClaimTypes.Role, persons.RoleId == 1 ? "admin" : persons.RoleId == 2 ? "utilisateur" : "participant"),
                new Claim(ClaimTypes.Email, persons.Email),
                new Claim("personId", persons.PersonId.ToString())
            };

            //Generation du token
            JwtSecurityToken jwt = new JwtSecurityToken(
                claims: myClaims,
                signingCredentials: credentials,
                issuer: "https://monapi.com", //Emetteur du token
                audience: "https://monclient.com", //Consommateur
                expires: DateTime.Now.AddDays(1)
                );

            //Produire le token sous forme de string
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwt);
        }


        public int GetIdFromToken(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];
            if (token is null) return 0;
            string tokenOK = token.Substring(7, token.Length - 7);
            JwtSecurityToken jwt = new JwtSecurityToken(tokenOK);
            return int.Parse(jwt.Claims.First(c => c.Type == "personId").Value);
        }

    }
}
