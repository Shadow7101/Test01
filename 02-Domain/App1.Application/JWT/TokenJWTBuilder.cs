using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace App1.Application.JWT
{
    public class TokenJWTBuilder
    {
        //chave gerada para o token
        private SecurityKey securityKey = null;
        //nome da aplicação
        private string subject = string.Empty;
        //remetente do token
        private string issuer = string.Empty;
        //receptor do token
        private string audience = string.Empty;
        //autoridades do usuário
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        //tem que o token demora para expirar
        private int expiryInMinutes = 5;

        public TokenJWTBuilder AddSecurityKey(SecurityKey _securityKey)
        {
            this.securityKey = _securityKey;
            return this;
        }
        public TokenJWTBuilder AddSubject(string _subject)
        {
            this.subject = _subject;
            return this;
        }
        public TokenJWTBuilder AddIssuer(string _issuer)
        {
            this.issuer = _issuer;
            return this;
        }
        public TokenJWTBuilder AddAudience(string _audience)
        {
            this.audience = _audience;
            return this;
        }
        public TokenJWTBuilder AddClaims(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }
        public TokenJWTBuilder AddClaims(Dictionary<string, string> _claims)
        {
            this.claims.Union(_claims);
            return this;
        }
        public TokenJWTBuilder AddExpiryInMinutes(int _expiryInMinutes)
        {
            this.expiryInMinutes = _expiryInMinutes;
            return this;
        }
        public TokenJWT Builder()
        {
            EnsureArguments();

            var _claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, this.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(x => new Claim(x.Key, x.Value)));


            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: _claims,
                expires: DateTime.UtcNow.AddMinutes(this.expiryInMinutes),
                signingCredentials: new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenJWT(token);
        }
        private void EnsureArguments()
        {
            if (this.securityKey == null) throw new Exception("A chave de criptografia não é válida!");
            if (string.IsNullOrEmpty(subject)) new Exception("O \"subject\" não é válido!");
            if (string.IsNullOrEmpty(issuer)) new Exception("O \"issuer\" não é válido!");
            if (string.IsNullOrEmpty(audience)) new Exception("O \"audience\" não é válido!");
        }
    }
}