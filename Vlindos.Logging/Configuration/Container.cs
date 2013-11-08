using Vlindos.Common.Notifications;

namespace Vlindos.Logging.Configuration
{
    public interface IConfigurationContainerFactory
    {
        IConfigurationContainer GetConfigurationContainer(Configuration configuration, IReader reader);
    }

    public interface IConfigurationContainer
    {
        INotifyManager ChangeNotifier { get; }
        IReader Reader { get; }
        Configuration Configuration { get; set; }
    }

    public class ConfigurationContainer : IConfigurationContainer
    {
        public ConfigurationContainer(
            INotifyManagerFactory notifyManagerFactory, IReader reader, Configuration configuration)
        {
            ChangeNotifier = notifyManagerFactory.GetNotifyManager();
            Reader = reader;
            Configuration = configuration;
        }

        public INotifyManager ChangeNotifier { get; private set; }

        public IReader Reader { get; private set; }

        public Configuration Configuration { get; set; }

    }
}
