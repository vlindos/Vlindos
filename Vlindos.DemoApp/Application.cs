using System;
using System.IO;
using Vlindos.Common.Configuration;
using Vlindos.Logging;
using Vlindos.Logging.Configuration;

namespace Vlindos.DemoApp
{
    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private readonly IFileConfigurationContainerGetterFactory<Configuration> _configurationContainerGetterFactory;
        private readonly IFileReaderFactory<Configuration> _fileConfigurationReaderFactory;
        private readonly ISystemFactory _systemFactory;

        public Application(
            IFileConfigurationContainerGetterFactory<Configuration> configurationContainerGetterFactoryFactory,
            IFileReaderFactory<Configuration> fileConfigurationReaderFactory,
            ISystemFactory systemFactory)
        {
            _configurationContainerGetterFactory = configurationContainerGetterFactoryFactory;
            _fileConfigurationReaderFactory = fileConfigurationReaderFactory;
            _systemFactory = systemFactory;
        }

        public void Run()
        {
            IContainer<Configuration> loggingConfigurationContainer;
            
            var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var directory = Path.GetDirectoryName(path) ?? "";
            var filePath = Path.Combine(directory, "Logging.config");

            var fileReader = _fileConfigurationReaderFactory.GetFileReader(filePath);

            if (_configurationContainerGetterFactory.GetFileConfigurationContainerGetter(filePath)
                    .GetContainer(fileReader, out loggingConfigurationContainer) == false)
            {
                return;
            }
            var loggingSystem = _systemFactory.GetSystem(loggingConfigurationContainer);

            if (loggingSystem.Start() == false) return;

            Console.WriteLine("Press any key to stop the application.");
            Console.ReadKey();

            loggingSystem.Stop();
        }
    }
}
