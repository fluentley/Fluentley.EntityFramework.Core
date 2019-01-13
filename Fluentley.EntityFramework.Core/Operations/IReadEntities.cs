using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fluentley.QueryBuilder.Models;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.EntityFramework.Core.Operations
{
    internal interface IReadEntities
    {
        Task<IQueryResult<T>> QueryAsync<T>(Action<IQueryOption<T>> options = null) where T : class;
       // Task<IQueryResult<TSelect>> QueryAsync<T,TSelect>(Action<IQueryOption<T>> options = null) where T : class;

        Task<T> SingleAsync<T>(Action<IQueryOption<T>> options = null) where T : class;
        Task<int> CountAsync<T>(Action<IQueryOption<T>> options = null) where T : class;
        Task<decimal> SumAsync<T>(Expression<Func<T, decimal>> sum, Action<IQueryOption<T>> options = null) where T : class;
        Task<decimal> AverageAsync<T>(Expression<Func<T, decimal>> average, Action<IQueryOption<T>> options = null) where T : class;

        Task<bool> AnyAsync<T>(Action<IQueryOption<T>> options = null) where T : class;
        Task<bool> AllAsync<T>(Expression<Func<T, bool>> predicate, Action<IQueryOption<T>> options = null) where T : class;
        Task<T> FindAsync<T, TId>(TId id) where T : class;
        Task<bool> ContainsAsync<T>(T contains, Action<IQueryOption<T>> options = null) where T : class;
    }
}