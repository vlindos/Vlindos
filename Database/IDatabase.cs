using System;
using Database.Entity;
using Database.Operations.Results;

namespace Database
{
    public interface IDatabase<T> : IDatabaseOperations<T>, IDisposable 
        where T : IEntity
    {
        ITransactionOperationResult<T> ExecuteInTranscation(
            TimeSpan timeout, Func<ITransacationalDatabaseOperations<T>, Transaction> actions);
    }
}
