namespace Vlindos.Logging.Configuration
{
    public interface IConfigurationContainer
    {
        Configuration Configuration { get; set; }
    }

    public class ConfigurationContainer : IConfigurationContainer
    {
        public ConfigurationContainer()
        {
            Configuration = new Configuration();
        }
        public Configuration Configuration { get; set; }
    }
}