using System;

namespace Database
{
    public interface ICriteriaOperation<T, out TReturn> 
        where T : IEntity
    {
        TReturn Where(Predicate<EntityHolder<T>> whereCriteria);

        TReturn Top(long top);

        TReturn Offset(long offset);

        TReturn OrderBy(Func<EntityHolder<T>, object> property, OrderType orderType);
    }
}