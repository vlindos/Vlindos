using System.Collections.Generic;

namespace Database.Datatypes
{
    public interface IEnumerableDataType<T> : IDataType<IEnumerable<T>>
    {
    }
}