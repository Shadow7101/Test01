using System;

namespace App1.Domain.ViewModel
{
    public class RememberPasswordViewModel
    {
        public Guid TokenId {get;set;}
        public string Password {get;set;}
        public string ConfirmPassword {get;set;}
        public string Ip { get; set; }
    }
}