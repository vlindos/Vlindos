using System;
using System.Collections.Generic;
using Database.Entity;

namespace Database.Operations.Results
{
    public interface IRetrieveOperationResult<T> : IOperationResult<T>, IDisposable
        where T : IEntity
    {
        void RetrieveNextBatch();

        IEnumerable<EntityHolder<T>> Entities { get; set; }
    }
}