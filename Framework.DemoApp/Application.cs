using System;
using Vlindos.Common.Extensions.String;
using Vlindos.Logging.Tools;

namespace Vlindos.DemoApp
{
    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private readonly IFileConfigurationLoggingSystemInitializer _loggingSystemInitializer;

        public Application(IFileConfigurationLoggingSystemInitializer loggingSystemInitializer)
        {
            _loggingSystemInitializer = loggingSystemInitializer;
        }

        public void Run()
        {
            var loggingSystem = _loggingSystemInitializer.GetLoggingSystem();

            Console.WriteLine("Press {0} to stop the application.".Args("any key"));
            Console.ReadKey();

            loggingSystem.Stop();
        }
    }
}
