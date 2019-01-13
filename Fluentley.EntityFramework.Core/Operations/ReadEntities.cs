using System;
using System.Linq.Expressions;
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

       

        public Task<T> SingleAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return queryResult.Data.FirstOrDefaultAsync();
        }

        public Task<int> CountAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return queryResult.Data.CountAsync();
        }

        public Task<decimal> SumAsync<T>(Expression<Func<T, decimal>> sum, Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return queryResult.Data.SumAsync(sum);
        }

        public Task<decimal> AverageAsync<T>(Expression<Func<T, decimal>> average, Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return queryResult.Data.AverageAsync(average);
        }

        public Task<bool> ContainsAsync<T>(T contains, Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return queryResult.Data.ContainsAsync(contains);
        }

        public Task<bool> AnyAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return queryResult.Data.AnyAsync();
        }

        public Task<bool> AllAsync<T>(Expression<Func<T, bool>> predicate, Action<IQueryOption<T>> options = null) where T : class
        {
            var queryResult = _context.Queryable<T>().QueryOn(options);

            foreach (var eagerLoad in queryResult.EagerLoads)
                queryResult.Data.Include(eagerLoad);

            return queryResult.Data.AllAsync(predicate);
        }

        public Task<T> FindAsync<T, TId>(TId id) where T : class
        {
            return _context.Set<T>().FindAsync(id);
        }
    }
}