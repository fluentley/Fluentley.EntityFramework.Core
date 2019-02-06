using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core.ResultArguments;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.EntityFramework.Core.Repository
{
    internal class Repository<T, TId> : IRepository<T, TId> where T : class
    {
        private readonly DataAccessService _service;

        #region Commands

        public Repository(DataAccessService service)
        {
            _service = service;
        }

        public Task<IResult<T>> Create(T model)
        {
            return _service.CreateAsync(model);
        }

        public Task<IResult<T>> Update(T model)
        {
            return _service.UpdateAsync(model);
        }

        public async Task<IResult<T>> Update(TId id, Dictionary<string, object> propertyValues)
        {
            var result = await Find(id);
            return await _service.UpdateViaPropertyValuesAsync(result.Data, propertyValues);
        }

        public Task<IResult<T>> Delete(T model)
        {
            return _service.DeleteAsync(model);
        }

        #endregion

        #region Queries

        public Task<IResult<T>> Find(TId id)
        {
            return _service.FindAsync<T, TId>(id);
        }

        public Task<IResult<IQueryable<T>>> Query(Action<IQueryOption<T>> options = null)
        {
            return _service.QueryAsync(options);
        }

        public Task<IResult<IQueryable<TSelect>>> Query<TSelect>(Expression<Func<T, TSelect>> selector, Action<IQueryOption<T>> options = null)
        {
            return _service.QueryAsync(selector, options);
        }

        public Task<IResult<T>> Single(Action<IQueryOption<T>> options = null)
        {
            return _service.SingleAsync(options);
        }

        public Task<IResult<TSelect>> Single<TSelect>(Expression<Func<T, TSelect>> selector, Action<IQueryOption<T>> options = null)
        {
            return _service.SingleAsync(selector, options);
        }

        #endregion
    }
}