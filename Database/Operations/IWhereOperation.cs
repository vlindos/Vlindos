using System;
using Database.Entity;

namespace Database.Operations
{
    public interface IWhereOperation<T, out TOperation>
        where T : IEntity
    {
        TOperation Where(Predicate<EntityHolder<T>> whereCriteria);
    }
}