using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fluentley.EntityFramework.Core.Models
{
    public interface IAudited<T>
    {
        T Data { get; set; }
        AuditEntry Audit { get; set; }
    }

    internal class Audited<T> : IAudited<T>
    {
        public Audited(T model, EntityEntry entry)
        {
            Data = model;
            Audit = new AuditEntry(entry);
        }

        public T Data { get; set; }
        public AuditEntry Audit { get; set; }
    }
}