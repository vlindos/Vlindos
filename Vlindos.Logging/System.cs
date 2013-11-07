using System;
using System.Collections.Generic;
using Vlindos.Common.Logging;
using Vlindos.Logging.Configuration;

namespace Vlindos.Logging
{
    public interface ISystemFactory
    {
        ISystem GetSystem(string filePath);
    }

    public interface ISystem
    {
        bool Start(List<string> messages);
        bool Restart(List<string> messages);
        void Stop(List<string> messages);
    }

    public class System : ISystem
    {
        private readonly IConfigurationContainer _configurationContainer;
        private readonly IReader _reader;
        private readonly ILogger _logger;
        private readonly IMessagesDequeuer _messagesDequeuer;
        private readonly IFileChangeWatcherFactory _fileChangeWatcherFactory;
        private readonly string _filePath;

        public System(IReader reader, 
                      ILogger logger,
                      IMessagesDequeuer messagesDequeuer,
                      IConfigurationContainer configurationContainer,
                      IFileChangeWatcherFactory fileChangeWatcherFactory,
                      string filePath)
        {
            _configurationContainer = configurationContainer;
            _reader = reader;
            _logger = logger;
            _messagesDequeuer = messagesDequeuer;
            _fileChangeWatcherFactory = fileChangeWatcherFactory;
            _filePath = filePath;
        }

        public bool Start(List<string> messages)
        {
            Configuration.Configuration configuration;
            var result = _reader.Read(_filePath, messages, out configuration);
            if (!result) return false;
            _configurationContainer.Configuration = configuration;

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

            _fileChangeWatcherFactory.GetFileChangeWatcher(_filePath, () =>
            {
                var msgs = new List<string>();
                Restart(msgs);
                if (msgs.Count > 0)
                {
                    _logger.Debug("The following unexpected messages occured " +
                                  "while trying to re-initialize logging:{0}{1}",
                                  Environment.NewLine, string.Join(Environment.NewLine, msgs));
                }
            });

            return true;
        }

        public bool Restart(List<string> messages)
        {
            Configuration.Configuration configuration;
            if (_reader.Read(_filePath, messages, out configuration) == false)
            {
                _logger.Error("Logging re-initizalization failed because failure(s) " +
                              "while re-reading configuration file:{0}{1}",
                              Environment.NewLine, string.Join(Environment.NewLine, messages));
                return false;
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

            return true;
        }

        public void Stop(List<string> messages)
        {
            _messagesDequeuer.Stop();
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                outputPipe.OutputEngine.Stop(messages);
            }
        }
    }
}
