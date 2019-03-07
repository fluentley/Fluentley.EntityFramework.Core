using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Fluentley.EntityFramework.Core.Operations
{
    internal class WriteEntities : IWriteEntities
    {
        private readonly DbContext _context;

        public WriteEntities(DbContext context)
        {
            _context = context;
        }

        public async Task<IAudited<T>> CreateAsync<T>(T model) where T : class
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Added;
            var auditResult = new Audited<T>(model, entry);
            await _context.SaveChangesAsync();
            entry.State = EntityState.Detached;

            return auditResult;
        }

        public async Task<IAudited<T>> UpdateByPropertyValues<T>(T model, IDictionary<string, object> propertyValues)
            where T : class
        {
            var entry = _context.Entry(model);
            var databaseValues = await entry.GetDatabaseValuesAsync();
            entry.OriginalValues.SetValues(databaseValues);
            entry.State = EntityState.Modified;
            entry.CurrentValues.SetValues(propertyValues);
            var auditedResult = new Audited<T>(model, entry);

            if (entry.Properties.Any(x => x.IsModified)) await _context.SaveChangesAsync();
            return auditedResult;
        }

        public async Task<IAudited<T>> UpdateAsync<T>(T model) where T : class
        {
            var entry = _context.Entry(model);
            var databaseValues = await entry.GetDatabaseValuesAsync();
            entry.OriginalValues.SetValues(databaseValues);
            entry.State = EntityState.Modified;

            foreach (var entryCurrentValue in entry.Properties)
                if (entryCurrentValue.OriginalValue.Equals(entryCurrentValue.CurrentValue))
                    entryCurrentValue.IsModified = false;

            var auditedResult = new Audited<T>(model, entry);

            if (entry.Properties.Any(x => x.IsModified))
                await _context.SaveChangesAsync();

            entry.State = EntityState.Detached;

            return auditedResult;
        }

        public async Task<IAudited<T>> DeleteAsync<T>(T model) where T : class
        {
            var entry = _context.Entry(model);
            entry.OriginalValues.SetValues(await entry.GetDatabaseValuesAsync());
            entry.State = EntityState.Deleted;
            var auditResult = new Audited<T>(model, entry);
            await _context.SaveChangesAsync();
            return auditResult;
        }
    }
}