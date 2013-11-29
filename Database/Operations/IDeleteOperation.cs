using Database.Entity;
using Database.Operations.Results;

namespace Database.Operations
{
    public interface IDeleteOperation<T> : ICriteriaOperation<T, IDeleteOperation<T>> 
        where T : IEntity
    {
        IOperationResult<T> Perform();
    }
}