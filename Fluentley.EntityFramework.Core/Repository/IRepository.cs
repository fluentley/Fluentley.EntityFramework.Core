using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core.ResultArguments;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.EntityFramework.Core.Repository
{
    public interface IRepository<T, in TId> where T : class
    {
        Task<IResult<T>> Create(T model);
        Task<IResult<T>> Update(T model);
        Task<IResult<T>> Delete(T model);
        Task<IResult<T>> Find(TId id);
        Task<IResult<IQueryable<T>>> Query(Action<IQueryOption<T>> options = null);
        Task<IResult<T>> Single(Action<IQueryOption<T>> options = null);
        Task<IResult<T>> Update(TId id, Dictionary<string, object> propertyValues);
    }
}