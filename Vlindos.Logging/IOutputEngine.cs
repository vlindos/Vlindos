using Vlindos.Common.Settings;

namespace Vlindos.Logging
{
    public interface IOutputEngine
    {
        bool ReadConfiguration(IXmlSettingsProvider outputXmlConfiguration);

        bool Start();

        void Stop();

        Message[] SaveMessages(params Message[] messages);
    }
}