using System;
using System.Threading.Tasks;
using Fluentley.QueryBuilder.Models;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.EntityFramework.Core.Operations
{
    internal interface IReadEntities
    {
        Task<T> FindAsync<T, TId>(TId id) where T : class;
        Task<IQueryResult<T>> QueryAsync<T>(Action<IQueryOption<T>> options = null) where T : class;
        Task<T> SingleAsync<T>(Action<IQueryOption<T>> options = null) where T : class;
    }
}