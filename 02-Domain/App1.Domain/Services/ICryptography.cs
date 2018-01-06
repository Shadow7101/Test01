using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Domain.Services
{
    public interface ICryptography
    {
        string Encrypt(string text);
    }
}
