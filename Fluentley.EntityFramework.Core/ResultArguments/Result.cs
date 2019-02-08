using System;
using Fluentley.EntityFramework.Core.Models;
using Fluentley.QueryBuilder.Models;
using Newtonsoft.Json;

namespace Fluentley.EntityFramework.Core.ResultArguments
{
    public class Result<T> : IResult<T>
    {
        [JsonIgnore] public AuditEntry Audit { get; set; }
        public bool IsSuccess { get; set; }
        public TimeSpan ExecutionDuration { get; set; }
        public string ExceptionMessage { get; set; }
        public T Data { get; set; }
        public ErrorType ErrorType { get; set; }
        public Exception Exception { get; set; }
        public QueryPaging Paging { get; set; }
        public string ModelType => typeof(T).Name;
    }
}