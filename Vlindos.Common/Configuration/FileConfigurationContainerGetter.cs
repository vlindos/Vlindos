using Vlindos.Common.Notifications;
using Vlindos.Common.Utilities.File;

namespace Vlindos.Common.Configuration
{
    public interface IFileConfigurationContainerGetterFactory<T>
    {
        IFileConfigurationContainerGetter<T> GetFileConfigurationContainerGetter(string filePath);
    }

    public interface IFileConfigurationContainerGetter<T> : IContainerGetter<T>
    {
    }

    public class FileConfigurationContainerGetter<T> : IFileConfigurationContainerGetter<T>
    {
        private readonly IFileChangeWatcherFactory _fileChangeWatcherFactory;
        private readonly IContainerFactory<T> _containerFactory;
        private readonly INotifyManagerFactory _notifyManagerFactory;
        private readonly string _filePath;

        public FileConfigurationContainerGetter(
            IFileChangeWatcherFactory fileChangeWatcherFactory,
            IContainerFactory<T> containerFactory, 
            INotifyManagerFactory notifyManagerFactory,
            string filePath)
        {
            _fileChangeWatcherFactory = fileChangeWatcherFactory;
            _containerFactory = containerFactory;
            _notifyManagerFactory = notifyManagerFactory;
            _filePath = filePath;
        }

        public bool GetContainer(
            IReader<T> reader, out IContainer<T> container)
        {
            T configuration;
            if (reader.Read(out configuration) == false)
            {
                container = null;
                return false;
            }

            container = _containerFactory.GetContainer(_notifyManagerFactory.GetNotifyManager(), configuration, reader);
            var containerCopy = container;
            _fileChangeWatcherFactory.GetFileChangeWatcher(_filePath, () => containerCopy.ChangeNotifier.Inform());
            
            return true;
        }
    }
}
