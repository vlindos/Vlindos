using System;
using Database.Entity;
using Database.Operations.Results;

namespace Database
{
    public interface ITransacationalDatabaseOperations<T> : IDatabaseOperations<T>, IOperationResult<T>, IDisposable
        where T : IEntity
    {
    }
}