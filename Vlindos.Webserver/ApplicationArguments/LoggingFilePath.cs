using Vlindos.Common.CommadLine;

namespace Vlindos.Webserver.ApplicationArguments
{
    public class LoggingFilePath : IApplicationArgument
    {
        public string Id 
        {
            get
            {
                return CommandLineArgument.LoggingConfigurationFile;
            }
        }

        public string ShortCommand {
            get
            {
                return "-l";
            }
        }
        public string LongCommand 
        {
            get
            {
                return "--log-config-file";
            }
        }

        public bool ExpectsValue
        {
            get
            {
                return true;
            }
        }

        public string DefaultValue
        {
            get
            {
                return "Logging.config";
            }
        }

        public string HelpMessage
        {
            get
            {
                return "Specifies file path to logging configuration.";
            }
        }
    }
}
