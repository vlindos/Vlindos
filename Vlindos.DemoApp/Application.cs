using System;
using System.Collections.Generic;
using System.IO;
using Vlindos.Logging;
using Vlindos.Logging.Configuration;

namespace Vlindos.DemoApp
{
    public interface IApplication
    {
        void Run(string[] args);
    }

    public class Application : IApplication
    {
        private readonly IFileConfigurationContainerCreator _configurationContainerCreator;
        private readonly ISystemFactory _systemFactory;

        public Application(
            IFileConfigurationContainerCreator configurationContainerCreator,
            ISystemFactory systemFactory)
        {
            _configurationContainerCreator = configurationContainerCreator;
            _systemFactory = systemFactory;
        }

        public void Run(string[] args)
        {
            var messages = new List<string>();
            IConfigurationContainer loggingConfigurationContainer;
            
            var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var directory = Path.GetDirectoryName(path) ?? "";
            var filePath = Path.Combine(directory, "Logging.config");

            if (_configurationContainerCreator
                    .GetConfiguration(messages, out loggingConfigurationContainer, filePath) == false)
            {
                messages.ForEach(Console.WriteLine);
                return;
            }
            var loggingSystem = _systemFactory.GetSystem(loggingConfigurationContainer);

            if (loggingSystem.Start(messages) == false) return;
            messages.ForEach(Console.WriteLine);

            Console.WriteLine("Press any key to stop the application.");
            Console.ReadKey();

            loggingSystem.Stop(messages);
            messages.ForEach(Console.WriteLine);
        }
    }
}
