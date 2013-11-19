using Vlindos.Common.Configuration;
using Vlindos.Common.Settings;

namespace Vlindos.Webserver.Configuration
{
    public class FileReader : IFileReader<Configuration>
    {
        private readonly ISettingReaderFactory _settingReaderFactory;
        private readonly IXmlSettingsProviderFactory _settingsProviderFactory;
        private readonly string _filePath;

        public FileReader(
            ISettingReaderFactory settingReaderFactory,
            IXmlSettingsProviderFactory settingsProviderFactory,
            string filePath)
        {
            _settingReaderFactory = settingReaderFactory;
            _settingsProviderFactory = settingsProviderFactory;
            _filePath = filePath;
        }

        public bool Read(out Configuration configuration)
        {
            //XmlElement xmlConfiguration;
            //try
            //{
            //    var xmlDocument = new XmlDocument();
            //    xmlDocument.Load(_filePath);
            //    xmlConfiguration = xmlDocument.DocumentElement;
            //}
            //catch (Exception exception)
            //{
            //    messages.Add(string.Format("Unable to read file at '{0}'. Because of:{1}{2}",
            //        _filePath, Environment.NewLine, exception));
            //    configuration = null;
            //    return false;
            //}

            configuration = null;
            return true;

            //var xmlSettingsProvider =
            //    _settingsProviderFactory.GetXmlSettingsProvider(xmlConfiguration);
            //var globalSettingsReader = _settingReaderFactory.GetSettingReader(xmlSettingsProvider);

            //bool enabled;
            //if (globalSettingsReader.ReadSetting("/logging[enabled]", () => true,
            //    (string s, out bool o) => bool.TryParse(s, out o), out enabled) == false)
            //{
            //    messages.Add(string.Format("Unable to read setting for xpath '/logging[enabled]'. " +
            //                               "Will use '{0}' value for it.", enabled));
            //}

            //configuration = new Configuration();

            //var firstMinimumLevelSet = false;
            //var nodes = xmlSettingsProvider.GetNodesForKey("/logging/output");
            //for (var i = 0; i < nodes.Count; i++)
            //{
            //    var outputXmlConfiguration = _settingsProviderFactory.GetXmlSettingsProvider(nodes[i]);
            //    var outputSettingsReader = _settingReaderFactory.GetSettingReader(outputXmlConfiguration);
            //    var type = outputSettingsReader.GetSetting("type");
            //    IOutput output = null;
            //    if (!string.IsNullOrWhiteSpace(type))
            //    {
            //        output =
            //            _outputs.FirstOrDefault(
            //                x => String.Compare(x.Type, type, StringComparison.OrdinalIgnoreCase) == 0);
            //    }
            //    if (output == null)
            //    {
            //        messages.Add(string.Format("Unable to read an output configuration with attribute type '{0}'. " +
            //                                   "The type must be specifying valid logging output engine.'. ", type));
            //        continue;
            //    }

            //    var outputPipe = new OutputPipe
            //    {
            //        Output = output,
            //        Queue = _queueFactory.GetQueue(),
            //        OutputEngine = output.GetEngine()
            //    };
            //    if (outputPipe.OutputEngine.ReadConfiguration(outputXmlConfiguration, messages))
            //    {
            //        messages.Add(string.Format("Unable to read an output configuration with attribute type '{0}'.", type));
            //        continue;
            //    }

            //    TimeSpan bufferMaximumKeepTime;
            //    if (globalSettingsReader.ReadSetting("bufferMaximumKeepTime", () => new TimeSpan(0, 0, 0, 25),
            //        (string s, out TimeSpan o) => TimeSpan.TryParse(s, out o), out bufferMaximumKeepTime) == false)
            //    {
            //        messages.Add(string.Format("Unable to attribute 'bufferMaximumKeepTime' for output of type '{0}'. " +
            //                                   "Will use '{1}' value for it.", type, bufferMaximumKeepTime));
            //    }
            //    outputPipe.BufferMaximumKeepTime = bufferMaximumKeepTime;
            //    configuration.OutputPipes.Add(outputPipe);

            //    Level minimumLogLevel;
            //    if (globalSettingsReader.ReadSetting("minimumLogLevel", () => Level.Debug,
            //        (string s, out Level o) => Enum.TryParse(s, out o), out minimumLogLevel) == false)
            //    {
            //        messages.Add(string.Format("Unable to attribute 'minimumLogLevel' for output of type '{0}'. " +
            //                                   "Will use '{1}' value for it.", type, minimumLogLevel));
            //    }
            //    outputPipe.MinimumLogLevel = minimumLogLevel;

            //    if (firstMinimumLevelSet == false)
            //    {
            //        firstMinimumLevelSet = true;
            //        configuration.MinimumLogLevel = outputPipe.MinimumLogLevel;
            //    }
            //    else if (outputPipe.MinimumLogLevel < configuration.MinimumLogLevel)
            //    {
            //        configuration.MinimumLogLevel = outputPipe.MinimumLogLevel;
            //    }
        }
    }
}
