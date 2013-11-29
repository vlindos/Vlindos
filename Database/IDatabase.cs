using System;
using Database.Entity;
using Database.Operations;
using Database.Operations.Results;

namespace Database
{
    public interface IDatabase<T> : IDisposable 
        where T : IEntity
    {
        IAddOperation<T> Add(params IEntity[] entity);
        IDeleteOperation<T> Delete();
        IUpdateOperation<T> Update(IEntity entity);
        IUpdateOperation<T> Update(Func<EntityHolder<T>, EntityHolder<T>> entityUpdater);
        ISelectOperation<T> Select();

        ITranscationOperation<T> BeginTranscation();
        IOperationResult<T> Commit();
    }

    public interface ITranscationOperation<T> : IOperationResult<T>, IDisposable
        where T : IEntity
    {
    }
}
