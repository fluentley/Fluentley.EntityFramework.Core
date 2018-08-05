using System.Collections.Generic;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core.Models;

namespace Fluentley.EntityFramework.Core.Operations
{
    internal interface IWriteEntities
    {
        Task<IAudited<T>> CreateAsync<T>(T model) where T : class;
        Task<IAudited<T>> DeleteAsync<T>(T model) where T : class;
        Task<IAudited<T>> UpdateAsync<T>(T model) where T : class;

        Task<IAudited<T>> UpdateByPropertyValues<T>(T model, IDictionary<string, object> propertyValues)
            where T : class;
    }
}