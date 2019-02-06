using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core.Operations;
using Fluentley.EntityFramework.Core.Processors;
using Fluentley.EntityFramework.Core.ResultArguments;
using Fluentley.QueryBuilder.Options;
using Microsoft.EntityFrameworkCore;

namespace Fluentley.EntityFramework.Core
{
    internal class DataAccessService
    {
        private readonly OperationProcessor _operationProcessor;
        private readonly IReadEntities _readEntitiesImplementation;
        private readonly IWriteEntities _writeEntitiesImplementation;

        public DataAccessService(DbContext context)
        {
            _readEntitiesImplementation = new ReadEntities(context);
            _writeEntitiesImplementation = new WriteEntities(context);
            _operationProcessor = new OperationProcessor();
        }

        #region Commands

        public Task<IResult<T>> CreateAsync<T>(T model) where T : class
        {
            return _operationProcessor.Process(() => _writeEntitiesImplementation.CreateAsync(model));
        }

        public Task<IResult<T>> UpdateAsync<T>(T model) where T : class
        {
            return _operationProcessor.Process(() => _writeEntitiesImplementation.UpdateAsync(model));
        }

        public Task<IResult<T>> UpdateViaPropertyValuesAsync<T>(T model, IDictionary<string, object> propertyValues)
            where T : class
        {
            return _operationProcessor.Process(() =>
                _writeEntitiesImplementation.UpdateByPropertyValues(model, propertyValues));
        }

        public Task<IResult<T>> DeleteAsync<T>(T model) where T : class
        {
            return _operationProcessor.Process(() => _writeEntitiesImplementation.DeleteAsync(model));
        }

        #endregion

        #region Queries

        public Task<IResult<T>> FindAsync<T, TId>(TId id) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.FindAsync<T, TId>(id));
        }

        public Task<IResult<IQueryable<T>>> QueryAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.QueryAsync(options));
        }

        public Task<IResult<IQueryable<TSelect>>> QueryAsync<T, TSelect>(Expression<Func<T, TSelect>> selector, Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.QueryAsync(options), selector);
        }

        public Task<IResult<T>> SingleAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.SingleAsync(options));
        }

        public Task<IResult<TSelect>> SingleAsync<T, TSelect>(Expression<Func<T, TSelect>> selector, Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.ProcessSingle(() => _readEntitiesImplementation.QueryAsync(options), selector);
        }

        public Task<IResult<int>> CountAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.CountAsync(options));
        }

        public Task<IResult<decimal>> SumAsync<T>(Expression<Func<T, decimal>> sum, Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.SumAsync(sum, options));
        }

        public Task<IResult<decimal>> AverageAsync<T>(Expression<Func<T, decimal>> average, Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.AverageAsync(average, options));
        }

        public Task<IResult<bool>> ContainsAsync<T>(T model, Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.ContainsAsync(model, options));
        }

        public Task<IResult<bool>> AnyAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.AnyAsync(options));
        }

        public Task<IResult<bool>> AllAsync<T>(Expression<Func<T, bool>> predicate, Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.AllAsync(predicate, options));
        }

        #endregion
    }
}