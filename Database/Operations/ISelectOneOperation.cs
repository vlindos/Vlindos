using Database.Entity;
using Database.Operations.Results;

namespace Database.Operations
{
    public interface ISelectOneOperation<T> : ISelectOneCriteriaOperation<T, ISelectOneOperation<T>>
        where T : IEntity
    {
        IRetriveOneOperationResult<T> RetrieveOne();
    }
}