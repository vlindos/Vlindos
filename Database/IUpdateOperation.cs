using System.Collections.Generic;

namespace Database
{
    public interface IUpdateOperation<T> : ICriteriaOperation<T, IUpdateOperation<T>>
        where T : IEntity
    {
        IEnumerable<T> Entities { get; set; }
        IResult<T> Perform();
    }
}