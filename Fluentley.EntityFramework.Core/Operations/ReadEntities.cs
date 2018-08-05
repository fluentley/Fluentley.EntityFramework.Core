using System;
using System.Threading.Tasks;
using Fluentley.QueryBuilder;
using Fluentley.QueryBuilder.Models;
using Fluentley.QueryBuilder.Options;
using Microsoft.EntityFrameworkCore;

namespace Fluentley.EntityFramework.Core.Operations
{
    internal class ReadEntities : IReadEntities
    {
        private readonly DbContext _context;

        public ReadEntities(DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public Task<IQueryResult<T>> QueryAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return Task.FromResult(queryResult);
        }

        public async Task<T> SingleAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return await queryResult.Data.FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync<T, TId>(TId id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}