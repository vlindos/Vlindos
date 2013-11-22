using Vlindos.Common.Notifications;

namespace Vlindos.Common.Configuration
{
    public interface IContainerFactory<T>
    {
        IContainer<T> GetContainer(INotifyManager notifyManager, T configuration, IReader<T> reader);
    }

    public interface IContainer<T>
    {
        INotifyManager ChangeNotifier { get; }
        IReader<T> Reader { get; }
        T Configuration { get; set; }
    }

    public class Container<T> : IContainer<T>
        where T : class
    {
        public Container(INotifyManager notifyManager, IReader<T> reader, T configuration)
        {
            ChangeNotifier = notifyManager;
            Reader = reader;
            Configuration = configuration;
        }

        public INotifyManager ChangeNotifier { get; private set; }

        public IReader<T> Reader { get; private set; }

        public T Configuration { get; set; }
    }
}
