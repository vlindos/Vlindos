using System;
using Database.Entity;
using Database.Operations;
using Database.Operations.Results;

namespace Database
{
    public enum Transaction
    {
        Rollback,
        Commit
    }

    public enum TransactionResult
    {
        OpenFailure,
        TimeoutFailure,
        ExceptionFailure,
        Rollbacked,
        CommitFailure,
        Committed
    }

    public interface IDatabaseOperations<T>
        where T : IEntity
    {
        IAddOperation<T> Add(params T[] entity);
        IDeleteOperation<T> Delete();
        IUpdateOperation<T> Update(T entity);
        IUpdateOperation<T> Update(Func<EntityHolder<T>, T> entityUpdater);
        ISelectOperation<T> Select();
    }

    public interface IDatabase<T> : IDatabaseOperations<T>, IDisposable 
        where T : IEntity
    {
        ITransactionOperationResult<T> ExecuteInTranscation(
            TimeSpan timeout, Func<ITransacationalDatabaseOperations<T>, Transaction> actions);
    }

    public interface ITransactionOperationResult<T> : IOperationResult<T>
        where T : IEntity
    {
        TransactionResult TransactionResult { get; set; }
        Exception ExceptionThrown { get; set; }
    }

    public interface ITransacationalDatabaseOperations<T> : IDatabaseOperations<T>, IOperationResult<T>, IDisposable
        where T : IEntity
    {
        IOperationResult<T> Commit();
    }
}
