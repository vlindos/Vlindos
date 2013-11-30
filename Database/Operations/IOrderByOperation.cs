using System;
using Database.Entity;

namespace Database.Operations
{
    public interface IOrderByOperation<T, out TOperation>
        where T : IEntity
    {
        TOperation OrderBy(Func<EntityHolder<T>, object> property, OrderType orderType);
    }
}