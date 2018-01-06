using System;
using App1.Domain.Entities.Base;

namespace App1.Domain.Entities
{
    public class User: AudithEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name {get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate {get;set;}
        public byte GenderId {get;set;}
        public virtual Gender Gender {get;set;}
        public byte RoleId {get;set;}
        public virtual Role Role {get;set;}
        public decimal Avaliation {get;set;}
        public bool EmailValidaded {get;set;}
        public DateTime? LastAccess {get;set;}
        public string LastAccessIp {get;set;}

        //dados de bloqueio do registro
        public bool Bloqued {get;set;}
        public DateTime? BloquedOn {get;set;}
        public string BloquedIp {get;set;}
        public Guid? BloquedBy {get;set;}

        //dados de banimento do registro
        public bool Banned {get;set;}
        public DateTime? BannedOn {get;set;}
        public string BannedIp {get;set;}
        public Guid? BannedBy {get;set;}
    }
}