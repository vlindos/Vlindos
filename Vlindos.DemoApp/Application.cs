using System;
using System.Collections.Generic;
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
            if (_configurationContainerCreator
                    .GetConfiguration(messages, out loggingConfigurationContainer, "Logging.config") == false)
            {
                messages.ForEach(Console.WriteLine);
                return;
            }
            var loggingSystem = _systemFactory.GetLoggingSystem(loggingConfigurationContainer);

            if (loggingSystem.Start(messages) == false) return;
            messages.ForEach(Console.WriteLine);

            Console.WriteLine("Press any key to stop the application.");
            Console.ReadKey();

            loggingSystem.Stop(messages);
            messages.ForEach(Console.WriteLine);
        }
    }
}
