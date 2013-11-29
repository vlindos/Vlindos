using System;

namespace Database
{
    public interface IRetrieveByBatchResult<T> : IRetrieveResult<T>, IDisposable
        where T : IEntity
    {
        void RetrieveNextBatch();
    }
}