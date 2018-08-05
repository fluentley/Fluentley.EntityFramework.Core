using System;
using System.Linq;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core.Repository;
using Fluentley.EntityFramework.Core.ResultArguments;
using Fluentley.QueryBuilder.Options;
using Microsoft.EntityFrameworkCore;

namespace Fluentley.EntityFramework.Core
{
    public static class DataAccessExtensions
    {
        #region Repository

        public static IQueryable<T> Queryable<T>(this DbContext context) where T : class
        {
            return context.Set<T>().AsNoTracking();
        }

        public static IRepository<T, TId> Repository<T, TId>(this DbContext context) where T : class
        {
            return new Repository<T, TId>(InitializeService(context));
        }

        #endregion

        #region Commands

        public static Task<IResult<T>> CreateAsync<T>(this DbContext context, T model) where T : class
        {
            return InitializeService(context).CreateAsync(model);
        }

        public static Task<IResult<T>> UpdateAsync<T>(this DbContext context, T model) where T : class
        {
            return InitializeService(context).UpdateAsync(model);
        }

        public static Task<IResult<T>> DeleteAsync<T>(this DbContext context, T model) where T : class
        {
            return InitializeService(context).DeleteAsync(model);
        }

        #endregion

        #region Queries

        public static Task<IResult<T>> FindAsync<T, TId>(this DbContext context, TId id) where T : class
        {
            return InitializeService(context).FindAsync<T, TId>(id);
        }

        public static Task<IResult<IQueryable<T>>> QueryAsync<T>(this DbContext context,
            Action<IQueryOption<T>> options = null) where T : class
        {
            return InitializeService(context).QueryAsync(options);
        }

        public static Task<IResult<T>> SingleAsync<T>(this DbContext context, Action<IQueryOption<T>> options = null)
            where T : class
        {
            return InitializeService(context).SingleAsync(options);
        }

        private static DataAccessService InitializeService(DbContext context)
        {
            return new DataAccessService(context);
        }

        #endregion
    }
}