using System.Collections.Generic;
using Database.Entity;
using Database.Operations.Results;

namespace Database.Operations
{
    public interface IUpdateOperation<T> : ICriteriaOperation<T, IUpdateOperation<T>>
        where T : IEntity
    {
        IEnumerable<T> Entities { get; set; }
        IOperationResult<T> Perform();
    }
}