using System;
using System.Collections.Generic;
using Vlindos.Common.Configuration;
using Vlindos.Common.Logging;
using Vlindos.Common.Notifications;

namespace Vlindos.Logging
{
    public interface ISystemFactory
    {
        ISystem GetSystem(IContainer<Configuration.Configuration> configurationContainer);
    }

    public interface ISystem
    {
        bool Start();
        void Stop();
    }

    public class System : ISystem, INotifiable
    {
        private readonly IContainer<Configuration.Configuration> _configurationContainer;
        private readonly ILogger _logger;
        private readonly IMessagesDequeuer _messagesDequeuer;

        public System(ILogger logger,
                      IMessagesDequeuer messagesDequeuer,
                      IContainer<Configuration.Configuration> configurationContainer)
        {
            _configurationContainer = configurationContainer;
            _logger = logger;
            _messagesDequeuer = messagesDequeuer;
        }

        public bool Start()
        {
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Start();
            }

            _messagesDequeuer.Start();

            _logger.Debug("Logging was initialized.");
                                    
            _configurationContainer.ChangeNotifier.Attach(this);

            return true;
        }


        public void Stop()
        {
            _configurationContainer.ChangeNotifier.Deattach(this);
            _messagesDequeuer.Stop();
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Stop();
            }
        }

        public void Inform()
        {
            var messages = new List<string>();
            Configuration.Configuration configuration;
            if (_configurationContainer.Reader.Read(out configuration) == false)
            {
                _logger.Error("Logging re-initizalization failed because failure(s) " +
                              "while re-reading configuration file.");
                return;
            }

            _messagesDequeuer.Stop();
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Stop();
            }

            _configurationContainer.Configuration = configuration;

            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Start();
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
