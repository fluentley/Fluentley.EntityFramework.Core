using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<IResult<T>> SingleAsync<T>(Action<IQueryOption<T>> options = null) where T : class
        {
            return _operationProcessor.Process(() => _readEntitiesImplementation.SingleAsync(options));
        }

        #endregion
    }
}