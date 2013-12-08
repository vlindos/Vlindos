using System;
using Vlindos.Logging;
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

            Console.WriteLine("Press any key to stop the application.");
            Console.ReadKey();

            loggingSystem.Stop();
        }
    }
}
