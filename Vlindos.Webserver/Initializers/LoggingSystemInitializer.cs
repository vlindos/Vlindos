using System;
using System.Collections.Generic;
using System.Linq;
using Vlindos.Common.CommadLine;
using Vlindos.Common.Configuration;
using Vlindos.Logging;

namespace Vlindos.Webserver.Initializers
{
    public interface ILoggingSystemInitializer
    {
        IDisposable Initialize(KeyValuePair<IApplicationArgument, List<string>> applicationArguments);
    }

    public class LoggingSystemInitializer : ILoggingSystemInitializer, IDisposable
    {
        private readonly IFileConfigurationContainerGetterFactory<Logging.Configuration.Configuration> 
            _configurationContainerGetterFactory;
        private readonly IFileReaderFactory<Logging.Configuration.Configuration> 
            _fileConfigurationReaderFactory;
        private readonly ISystemFactory _systemFactory;
        private ISystem _loggingSystem;

        public LoggingSystemInitializer(
            IFileConfigurationContainerGetterFactory<Logging.Configuration.Configuration>
                configurationContainerGetterFactoryFactory,
            IFileReaderFactory<Logging.Configuration.Configuration> fileConfigurationReaderFactory,
            ISystemFactory systemFactory)
        {
            _configurationContainerGetterFactory = configurationContainerGetterFactoryFactory;
            _fileConfigurationReaderFactory = fileConfigurationReaderFactory;
            _systemFactory = systemFactory;
        }
        public IDisposable Initialize(KeyValuePair<IApplicationArgument, List<string>> applicationArgument)
        {
            IContainer<Logging.Configuration.Configuration> loggingConfigurationContainer;

            var filePath = applicationArgument.Value.LastOrDefault() ?? applicationArgument.Key.DefaultValue;

            var fileReader = _fileConfigurationReaderFactory.GetFileReader(filePath);

            if (_configurationContainerGetterFactory.GetFileConfigurationContainerGetter(filePath)
                .GetContainer(fileReader, out loggingConfigurationContainer) == false)
            {
                return null;
            }
            _loggingSystem = _systemFactory.GetSystem(loggingConfigurationContainer);

            if (_loggingSystem.Start() == false) return null;

            return this;
        }

        public void Dispose()
        {
            _loggingSystem.Stop();
        }
    }
}
