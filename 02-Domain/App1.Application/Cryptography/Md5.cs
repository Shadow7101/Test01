using App1.Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace App1.Application.Cryptography
{
    public class Md5 : ICryptography
    {
        private readonly IConfiguration configuration;
        public Md5(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public string Encrypt(string text)
        {
            string _salt = configuration["Cryptography:CryptoKey"];

            if(!string.IsNullOrEmpty(_salt))
                text = text + _salt;

            MD5 md5Hash = MD5.Create();

            byte[] Hash1 = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));            

            string hashFinal1 = Encoding.Unicode.GetString(Hash1);

            return hashFinal1;
        }
    }
}