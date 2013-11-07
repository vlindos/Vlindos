using System.Collections.Specialized;
using System.Configuration;

namespace Vlindos.Common.Settings
{
    public class AppConfigSettingsProvider : ISettingsProvider
    {
        public NameValueCollection Settings 
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        public string GetValueForKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}