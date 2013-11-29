using Database.Entity;
using Database.Operations.Results;

namespace Database.Operations
{
    public interface IAddOperation<T>
        where T : IEntity
    {
        IOperationResult<T> Perform();
    }
}