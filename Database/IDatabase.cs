using System;

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
    }
}
