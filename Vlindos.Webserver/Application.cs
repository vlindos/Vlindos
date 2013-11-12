using System;
using System.Linq;
using Vlindos.Common.CommadLine;
using Vlindos.Common.Configuration;
using Vlindos.Webserver.ApplicationArguments;
using Vlindos.Webserver.Initializers;
using Vlindos.Webserver.Webserver;

namespace Vlindos.Webserver
{
    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private readonly IApplicationArgumentsGetter _applicationArgumentsGetter;
        private readonly ILoggingSystemInitializer _loggingSystemInitializer;
        private readonly IFileReader<Configuration.Configuration> _configurationReader;
        private readonly ITcpServer _tcpServer;
        private readonly IHttpRequestProcessor _httpRequestProcessor;

        public Application(
            IApplicationArgumentsGetter applicationArgumentsGetter,
            ILoggingSystemInitializer loggingSystemInitializer,
            IFileReader<Configuration.Configuration> configurationReader,
            ITcpServer tcpServer, IHttpRequestProcessor httpRequestProcessor)
        {
            _applicationArgumentsGetter = applicationArgumentsGetter;
            _loggingSystemInitializer = loggingSystemInitializer;
            _configurationReader = configurationReader;
            _tcpServer = tcpServer;
            _httpRequestProcessor = httpRequestProcessor;
        }

        public void Run()
        {
            var applicationArguments = _applicationArgumentsGetter.GetApplicationArguments();
            var loggingSystem = _loggingSystemInitializer.Initialize(
                applicationArguments.FirstOrDefault(x => x.Key.Id == CommandLineArgument.LoggingConfigurationFile));
            if (loggingSystem == null) return;
            using (loggingSystem)
            {
                Configuration.Configuration configuration;
                if (_configurationReader.Read(out configuration) == false) return;
                if (_tcpServer.Start(configuration.Binds, _httpRequestProcessor) == false) return;
                Console.WriteLine("Press any key to stop the application.");
                Console.ReadKey();

                _tcpServer.Stop();
            }
        }
    }
}
