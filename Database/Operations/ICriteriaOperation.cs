using System;
using Database.Entity;

namespace Database.Operations
{
    public interface ICriteriaOperation<T, out TOperation> 
        where T : IEntity
    {
        TOperation Where(Predicate<EntityHolder<T>> whereCriteria);

        TOperation Top(long top);

        TOperation Offset(long offset);

        TOperation OrderBy(Func<EntityHolder<T>, object> property, OrderType orderType);
    }
}