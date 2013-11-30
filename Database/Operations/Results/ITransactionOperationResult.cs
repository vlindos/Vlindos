using Database.Entity;

namespace Database.Operations.Results
{
    public interface ITransactionOperationResult<T> : IOperationResult<T>
        where T : IEntity
    {
        TransactionResult TransactionResult { get; set; }
    }
}