using System;
using System.IO;

namespace Vlindos.Logging
{
    public interface IFileChangeWatcherFactory
    {
        IFileChangeWatcher GetFileChangeWatcher(string filePath, Action action);
    }

    public interface IFileChangeWatcher
    {
    }

    public class FileChangeWatcher : IFileChangeWatcher, IDisposable
    {
        private readonly Action _action;
        private readonly FileSystemWatcher _watcher;

        public FileChangeWatcher(string filePath, Action action)
        {
            _action = action;
            _watcher = new FileSystemWatcher
            {
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Size,
                Filter = filePath
            };

            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;
        }

        public Action Action { get { return _action; } }
        
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            ((FileChangeWatcher)source).Action.Invoke();
        }

        public void Dispose()
        {
            _watcher.EnableRaisingEvents = false;
        }
    }
}