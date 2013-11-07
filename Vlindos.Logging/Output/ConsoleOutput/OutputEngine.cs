using System;
using System.Collections.Generic;
using Vlindos.Common.Settings;

namespace Vlindos.Logging.Output.ConsoleOutput
{
    public interface IOutputEngineFactory
    {
        IOutputEngine GetOutputEngine();
    }

    public class OutputEngine : IOutputEngine
    {
        private readonly ISettingReaderFactory _settingReaderFactory;
        private readonly IXmlSettingsProviderFactory _xmlSettingsProviderFactory;
        private readonly IMessageTextFormatter _messageTextFormatter;
        private readonly Dictionary<Level, ConsoleColor> _levelColors;

        public OutputEngine(ISettingReaderFactory settingReaderFactory, 
            IXmlSettingsProviderFactory xmlSettingsProviderFactory, 
            IMessageTextFormatter messageTextFormatter)
        {
            _settingReaderFactory = settingReaderFactory;
            _xmlSettingsProviderFactory = xmlSettingsProviderFactory;
            _messageTextFormatter = messageTextFormatter;
            _levelColors = new Dictionary<Level, ConsoleColor>();
            foreach (var level in Enum.GetValues(typeof(Level)))
            {
                _levelColors.Add((Level)level, ConsoleColor.Gray);
            }
        }

        public bool ReadConfiguration(IXmlSettingsProvider outputXmlConfiguration, List<string> messages)
        {
            var nodes = outputXmlConfiguration.GetNodesForKey("color");
            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                var colorForLevelSettingReader = _settingReaderFactory.GetSettingReader(
                    _xmlSettingsProviderFactory.GetXmlSettingsProvider(node));
                Level level;
                if (colorForLevelSettingReader.ReadSetting("[for]", (string s, out Level o) =>
                    {
                        if (Enum.TryParse(s, out o) == false)
                        {
                            messages.Add(string.Format("Unknown level type specifier '{0}' for color.", s));
                            return false;
                        }
                        return true;
                    }, out level) == false)
                {
                    continue;
                }
                ConsoleColor consoleColor;
                if (colorForLevelSettingReader.ReadSetting("[for]", (string s, out ConsoleColor o) =>
                    {
                        if (Enum.TryParse(s, out o) == false)
                        {
                            messages.Add(string.Format("Unknown color specifier '{0}' for color.", s));
                            return false;
                        }
                        return true;
                    }, out consoleColor) == false)
                {
                    continue;
                }
                _levelColors[level] = consoleColor;
            }

            return true;
        }

        public bool Start(List<string> messages)
        {
            // nothing needed to be done
            return true;
        }

        public void Stop(List<string> messages)
        {
            // nothing needed to be done
        }

        public Message[] SaveMessages(params Message[] messages)
        {
            foreach (var message in messages)
            {
                Console.ForegroundColor = _levelColors[message.Level];
                Console.WriteLine(_messageTextFormatter.GetMessageAsText(message));
            }
            return messages;
        }
    }
}
