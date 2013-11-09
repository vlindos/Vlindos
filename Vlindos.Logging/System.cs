using System;
using System.Collections.Generic;
using Vlindos.Common.Logging;
using Vlindos.Common.Notifications;
using Vlindos.Logging.Configuration;

namespace Vlindos.Logging
{
    public interface ISystemFactory
    {
        ISystem GetSystem(IConfigurationContainer configurationContainer);
    }

    public interface ISystem
    {
        bool Start(List<string> messages);
        void Stop(List<string> messages);
    }

    public class System : ISystem, INotifiable
    {
        private readonly IConfigurationContainer _configurationContainer;
        private readonly ILogger _logger;
        private readonly IMessagesDequeuer _messagesDequeuer;

        public System(ILogger logger,
                      IMessagesDequeuer messagesDequeuer,
                      IConfigurationContainer configurationContainer)
        {
            _configurationContainer = configurationContainer;
            _logger = logger;
            _messagesDequeuer = messagesDequeuer;
        }

        public bool Start(List<string> messages)
        {
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Start(messages);
            }

            _messagesDequeuer.Start();

            _logger.Debug("Logging was initialized.");

            if (messages.Count > 0)
            {
                _logger.Debug("The following unexpected messages occured " +
                              "while trying to initialize logging:{0}{1}",
                              Environment.NewLine, string.Join(Environment.NewLine, messages));
            }

            _configurationContainer.ChangeNotifier.Attach(this);

            return true;
        }


        public void Stop(List<string> messages)
        {
            _configurationContainer.ChangeNotifier.Deattach(this);
            _messagesDequeuer.Stop();
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Stop(messages);
            }
        }

        public void Inform()
        {
            var messages = new List<string>();
            Configuration.Configuration configuration;
            if (_configurationContainer.Reader.Read(messages, out configuration) == false)
            {
                _logger.Error("Logging re-initizalization failed because failure(s) " +
                              "while re-reading configuration file:{0}{1}",
                              Environment.NewLine, string.Join(Environment.NewLine, messages));
                return;
            }

            _messagesDequeuer.Stop();
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Stop(messages);
            }

            _configurationContainer.Configuration = configuration;

            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Start(messages);
            }
            _messagesDequeuer.Start();

            _logger.Info("Logging was re-initizalized.");             

            if (messages.Count <= 0) return;

            _logger.Debug("The following unexpected messages occured " +
                          "while trying to re-initialize logging:{0}{1}",
                          Environment.NewLine, string.Join(Environment.NewLine, messages));
        }
    }
}
