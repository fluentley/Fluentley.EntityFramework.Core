using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core.Models;
using Fluentley.EntityFramework.Core.ResultArguments;
using Fluentley.QueryBuilder.Models;

namespace Fluentley.EntityFramework.Core.Processors
{
    public class OperationProcessor
    {
        public async Task<IResult<T>> Process<T>(Func<Task<T>> operation)
        {
            var result = new Result<T>();
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                result.Data = await operation();
                result.IsSuccess = true;
                result.ExecutionDuration = watch.Elapsed;
            }
            catch (Exception exception)
            {
                result.ErrorType = ErrorType.DatabaseException;
                result.IsSuccess = false;
                result.ExceptionMessage = exception.InnerException?.Message ?? exception.Message;
                result.Exception = exception;
            }
            finally
            {
                watch.Stop();
                result.ExecutionDuration = watch.Elapsed;
            }

            return result;
        }

        public async Task<IResult<IQueryable<T>>> Process<T>(Func<Task<IQueryResult<T>>> operation)
        {
            var result = new Result<IQueryable<T>>();
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                var queryResult = await operation();

                result.Data = queryResult.Data;
                result.Paging = queryResult.Paging;
                result.IsSuccess = true;
                result.ExecutionDuration = watch.Elapsed;
            }
            catch (Exception exception)
            {
                result.ErrorType = ErrorType.DatabaseException;
                result.IsSuccess = false;
                result.ExceptionMessage = exception.InnerException?.Message ?? exception.Message;
                result.Exception = exception;
            }
            finally
            {
                watch.Stop();
                result.ExecutionDuration = watch.Elapsed;
            }

            return result;
        }

        public async Task<IResult<IQueryable<TSelect>>> Process<T, TSelect>(Func<Task<IQueryResult<T>>> operation, Expression<Func<T, TSelect>> selector)
        {
            var result = new Result<IQueryable<TSelect>>();
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                var queryResult = await operation();

                result.Data = queryResult.Data.Select(selector);
                result.Paging = queryResult.Paging;
                result.IsSuccess = true;
                result.ExecutionDuration = watch.Elapsed;
            }
            catch (Exception exception)
            {
                result.ErrorType = ErrorType.DatabaseException;
                result.IsSuccess = false;
                result.ExceptionMessage = exception.InnerException?.Message ?? exception.Message;
                result.Exception = exception;
            }
            finally
            {
                watch.Stop();
                result.ExecutionDuration = watch.Elapsed;
            }

            return result;
        }

        public async Task<IResult<T>> Process<T>(Func<Task<IAudited<T>>> operation)
        {
            var result = new Result<T>();
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                var operationResult = await operation();
                result.Audit = operationResult.Audit;
                result.Data = operationResult.Data;
                result.IsSuccess = true;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.ErrorType = ErrorType.DatabaseException;
                result.ExceptionMessage = exception.InnerException?.Message ?? exception.Message;
                result.Exception = exception;
            }
            finally
            {
                watch.Stop();
                result.ExecutionDuration = watch.Elapsed;
            }

            return result;
        }
    }
}