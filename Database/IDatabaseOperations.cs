using System;
using Database.Entity;
using Database.Operations;

namespace Database
{
    public interface IDatabaseOperations<T>
        where T : IEntity
    {
        IAddOperation<T> Add(params T[] entity);
        IDeleteOperation<T> Delete();
        IUpdateOperation<T> Update(T entity);
        IUpdateOperation<T> Update(Func<EntityHolder<T>, T> entityUpdater);
        ISelectOperation<T> Select();
        ISelectOneOperation<T> SelectOne();
    }
}