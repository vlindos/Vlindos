using System.Collections.Generic;

namespace Vlindos.Logging.Configuration
{
    public interface IConfigurationGetter
    {
        bool GetConfiguration(
            List<string> messages, out IConfigurationContainer configurationContainer, params object[] args);
    }
}