using System;
using Database.Entity;
using Database.Operations.Results;

namespace Database
{
    public interface IDatabase<T> : IDatabaseOperations<T>, IDisposable 
        where T : IEntity
    {
        ITransactionOperationResult<T> ExecuteInTranscation(Func<ITransacationalDatabaseOperations<T>, Transaction> actions, TimeSpan timeout);
    }
}
