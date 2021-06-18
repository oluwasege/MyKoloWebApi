using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace MyKoloWebApi.Utils
{
    public class JwtUtils
    {
        public static string WriteToken(string _issuer,string audience,string signingSecret,DateTime expiry)
        {
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingSecret));
            var token = new JwtSecurityToken(
                issuer:_issuer,
                audience:audience,
                expires:expiry
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
            

         
        }
    }
}
