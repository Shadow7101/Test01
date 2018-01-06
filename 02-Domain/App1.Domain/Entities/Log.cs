using System;

namespace App1.Domain.Entities
{
    public class Log
    {
        public long Id {get;set;}
        public string Email { get; set; }
        public Guid? CreatedBy {get;set;}
        public string CreatedIp {get;set;}
        public DateTime CreatedOn {get;set;}
        public byte LogTypeId {get; set; }
        public virtual LogType LogType {get;set;}
    }
}