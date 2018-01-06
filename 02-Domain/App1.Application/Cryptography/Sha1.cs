using App1.Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace App1.Application.Cryptography
{
    public class Sha1 : ICryptography
    {
        private readonly IConfiguration configuration;
        public Sha1(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public string Encrypt(string text)
        {
            string _salt = configuration["Cryptography:CryptoKey"];

            if(!string.IsNullOrEmpty(_salt))
                text = text + _salt;

            HashAlgorithm hashing = new SHA1Managed();

            byte[] Hash1 = hashing.ComputeHash(Encoding.UTF8.GetBytes(text));            

            string hashFinal1 = Encoding.Unicode.GetString(Hash1);

            return hashFinal1;
        }
    }
}