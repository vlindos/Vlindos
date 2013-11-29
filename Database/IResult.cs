using System.Collections.Generic;

namespace Database
{
    public interface IResult<T> 
        where T : IEntity
    {
        bool Success { get; set; }
        IEnumerable<string> Errors { get; set; }
    }
}