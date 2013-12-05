using System.Collections.Generic;
using Database.Entity;

namespace Database.Operations.Results
{
    public interface IOperationResult<T> 
        where T : IEntity
    {
        bool Success { get; set; }
        List<string> Messages { get; set; }
    }
}
