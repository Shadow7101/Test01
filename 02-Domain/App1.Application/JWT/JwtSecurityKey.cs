using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace App1.Application.JWT
{
    //dotnet add package IdentityModel --version 2.16.1
    //dotnet add package System.IdentityModel.Tokens.Jwt --version 5.1.5
    public class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}