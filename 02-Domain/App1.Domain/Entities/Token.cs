using System;

namespace App1.Domain.Entities
{
    public class Token
    {
        public Token()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedIp { get; set; }
        public string TokenData { get; set; }
        public int Validate { get; set; }
        public byte TypeId { get; set; }
        public virtual TokenType Type { get; set; }
    }
}