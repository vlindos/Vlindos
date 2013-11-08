using System.Collections.Generic;

namespace Vlindos.Logging.Configuration
{
    public interface IReader
    {
        bool Read(List<string> messages, out Configuration configuration);
    }
}