using System;

namespace App1.Domain.ViewModel
{
    public class LoginResultViewModel
    {
        public Guid TokenId { get; set; }
        public string TokenValue { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}