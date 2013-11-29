using System.Collections.Generic;

namespace Database
{
    public interface IRetrieveResult<T> : IResult<T>
        where T : IEntity
    {
        IEnumerable<EntityHolder<T>> Entities { get; set; }
    }
}