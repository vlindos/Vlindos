using System.IO;
using Vlindos.Common.Configuration;

namespace Vlindos.Logging.Tools
{
    public interface IFileConfigurationLoggingSystemInitializer
    {
        ISystem GetLoggingSystem();
    }

    public class FileConfigurationLoggingSystemInitializer : IFileConfigurationLoggingSystemInitializer
    {
        private readonly IFileConfigurationContainerGetterFactory<Configuration.Configuration> _configurationContainerGetterFactory;
        private readonly IFileReaderFactory<Configuration.Configuration> _fileConfigurationReaderFactory;
        private readonly ISystemFactory _systemFactory;

        public FileConfigurationLoggingSystemInitializer(
            IFileConfigurationContainerGetterFactory<Configuration.Configuration> configurationContainerGetterFactory,
            IFileReaderFactory<Configuration.Configuration> fileConfigurationReaderFactory,
            ISystemFactory systemFactory)
        {
            _configurationContainerGetterFactory = configurationContainerGetterFactory;
            _fileConfigurationReaderFactory = fileConfigurationReaderFactory;
            _systemFactory = systemFactory;
        }

        public ISystem GetLoggingSystem()
        {
            IContainer<Configuration.Configuration> loggingConfigurationContainer;

            var path = global::System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var directory = Path.GetDirectoryName(path) ?? "";
            var filePath = Path.Combine(directory, "Logging.config");

            var fileReader = _fileConfigurationReaderFactory.GetFileReader(filePath);

            if (_configurationContainerGetterFactory.GetFileConfigurationContainerGetter(filePath)
                .GetContainer(fileReader, out loggingConfigurationContainer) == false)
            {
                return null;
            }
            var loggingSystem = _systemFactory.GetSystem(loggingConfigurationContainer);

            if (loggingSystem.Start() == false) return null;

            return loggingSystem;
        }
    }
}