using System;
using System.IdentityModel.Tokens.Jwt;

namespace App1.Application.JWT
{
    public class TokenJWT
    {
        private JwtSecurityToken token;

        public TokenJWT() { }

        public TokenJWT(JwtSecurityToken _token) => this.token = _token;

        public DateTime ValidTo => token.ValidTo;

        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}