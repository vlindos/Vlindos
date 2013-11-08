using System.Collections.Generic;
using System.Linq;
using Vlindos.Common.Utilities.File;

namespace Vlindos.Logging.Configuration
{
    public interface IFileConfigurationContainerCreator : IConfigurationGetter
    {
    }

    public class FileConfigurationContainerCreator : IFileConfigurationContainerCreator
    {
        private readonly IFileReaderFactory _fileReaderFactory;
        private readonly IFileChangeWatcherFactory _fileChangeWatcherFactory;
        private readonly IConfigurationContainerFactory _configurationContainerFactory;

        public FileConfigurationContainerCreator(IFileReaderFactory fileReaderFactory, 
            IFileChangeWatcherFactory fileChangeWatcherFactory, 
            IConfigurationContainerFactory configurationContainerFactory)
        {
            _fileReaderFactory = fileReaderFactory;
            _fileChangeWatcherFactory = fileChangeWatcherFactory;
            _configurationContainerFactory = configurationContainerFactory;
        }

        public bool GetConfiguration(
            List<string> messages, out IConfigurationContainer configurationContainer, params object[] args)
        {
            var filePath = args.Select(x => x as string).FirstOrDefault();
            if (filePath == null)
            {
                messages.Add("Expecting third string argument specifiying the configuration's file path .");
                configurationContainer = null;
                return false;
            }

            var fileReader =_fileReaderFactory.GetFileReader(filePath);
            Configuration configuration;
            if (fileReader.Read(messages, out configuration) == false)
            {
                configurationContainer = null;
                return false;
            }

            configurationContainer = _configurationContainerFactory
                .GetConfigurationContainer(configuration, fileReader);
            var configurationContainerCopy = configurationContainer;
            _fileChangeWatcherFactory.GetFileChangeWatcher(
                filePath, () => configurationContainerCopy.ChangeNotifier.Inform());
            
            return true;
        }
    }
}
