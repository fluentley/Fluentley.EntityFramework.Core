using System;
using Fluentley.EntityFramework.Core.Models;
using Fluentley.QueryBuilder.Models;
using Newtonsoft.Json;

namespace Fluentley.EntityFramework.Core.ResultArguments
{
    public interface IResult
    {
        string ExceptionMessage { get; set; }
        TimeSpan ExecutionDuration { get; set; }
        bool IsSuccess { get; set; }
        ErrorType ErrorType { get; set; }

        [JsonIgnore] Exception Exception { get; set; }

        QueryPaging Paging { get; set; }

        [JsonIgnore] AuditEntry Audit { get; set; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; set; }
        string ModelType { get; }
    }

    public class uPaging
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPageIndex { get; set; }
    }
}