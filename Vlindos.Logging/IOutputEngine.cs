using System.Collections.Generic;
using Vlindos.Common.Settings;

namespace Vlindos.Logging
{
    public interface IOutputEngine
    {
        bool ReadConfiguration(IXmlSettingsProvider outputXmlConfiguration, List<string> messages);

        bool Start(List<string> messages);

        void Stop(List<string> messages);

        Message[] SaveMessages(params Message[] messages);
    }
}