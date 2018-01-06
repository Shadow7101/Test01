using System;

namespace App1.Domain.Entities.Base
{
    public abstract class AudithEntity
    {
        public AudithEntity()
        {
            this.CreatedOn = DateTime.Now;
        }
        //dados de auditoria de criação de registro
        public DateTime CreatedOn { get; set;}
        public string CreatedIp { get; set; }
        public Guid CreatedBy { get; set; }

        //dados sobre modificacao do registro
        public DateTime? ModifyOn {get;set;}
        public string ModifyIp {get;set;}
        public Guid? ModifyBy {get;set;}

        //dados de exclusão do registro
        public bool Deleted {get;set;}
        public DateTime? DeletedOn {get;set;}
        public string DeletedIp {get;set;}
        public Guid? DeletedBy {get;set;}
    }
}