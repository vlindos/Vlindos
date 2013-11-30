using Database.Entity;

namespace Database.Operations.Results
{
    public interface IRetriveOneOperationResult<T> : IOperationResult<T> 
        where T : IEntity
    {
        EntityHolder<T> Result { get; set; }
    }
}