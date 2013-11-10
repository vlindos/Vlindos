using System.Collections.Generic;
using System.IO;
using System.Text;
using Vlindos.Common.Settings;

namespace Vlindos.Logging.Output.FileOutput
{
    public interface IOutputEngineFactory
    {
        IOutputEngine GetOutputEngine();
    }

    public class OutputEngine : IOutputEngine
    {
        private readonly ISettingReaderFactory _settingReaderFactory;
        private readonly IFilePathGetterFactory _filePathGetterFactory;
        private readonly IMessageTextFormatter _messageTextFormatter;
        private readonly IInternalLogger _internalLogger;
        private IFilePathGetter _filePathGetter;
        private string _filePath;

        public OutputEngine(
            ISettingReaderFactory settingReaderFactory, 
            IFilePathGetterFactory filePathGetterFactory, 
            IMessageTextFormatter messageTextFormatter,
            IInternalLogger internalLogger)
        {
            _settingReaderFactory = settingReaderFactory;
            _filePathGetterFactory = filePathGetterFactory;
            _messageTextFormatter = messageTextFormatter;
            _internalLogger = internalLogger;
        }

        public bool ReadConfiguration(IXmlSettingsProvider outputXmlConfiguration)
        {
            var settingsReader = _settingReaderFactory.GetSettingReader(outputXmlConfiguration);
            _filePath = settingsReader.GetSetting("filePath");
            if (string.IsNullOrWhiteSpace(_filePath) == false)
            {
                _internalLogger.Log("'filePath' needs to be specified.");
                return false;
            }
            return true;
        }

        public bool Start()
        {
            _filePathGetter = _filePathGetterFactory.GetFilePathGetter(_filePath);
            // nothing needed to be done
            return true;
        }

        public void Stop()
        {
            // nothing needed to be done
        }

        public Message[] SaveMessages(params Message[] messages)
        {
            var outputFileMessages = new Dictionary<string, List<Message>>();
            foreach (var message in messages)
            {
                var filePath = _filePathGetter.GetFilePath(message);
                if (outputFileMessages.ContainsKey(filePath))
                {
                    outputFileMessages[filePath].Add(message);
                }
                else
                {
                    outputFileMessages.Add(filePath, new List<Message>{message});
                }
            }
            foreach (var filePath in outputFileMessages.Keys)
            {
                var sb = new StringBuilder();
                foreach (var message in outputFileMessages[filePath])
                {
                    sb.Append(_messageTextFormatter.GetMessageAsText(message));
                }
                File.AppendAllText(filePath, sb.ToString());
            }
            return messages;
        }
    }
}
