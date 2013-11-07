using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace Vlindos.Common.Settings
{
    public interface IConfigFileSettingsProviderFactory
    {
        IConfigFileSettingsProvider GetConfigFileSettingsProvider(string filePath);
    }

    public interface IConfigFileSettingsProvider : ISettingsProvider
    {
    }

    public class ConfigFileSettingsProvider : IConfigFileSettingsProvider
    {
        private readonly NameValueCollection _settings;
        public ConfigFileSettingsProvider(string filePath)
        {
            var fileContents = File.ReadAllText(filePath);

            _settings = new NameValueCollection();

            foreach (var fileLine in Regex.Split(fileContents, Environment.NewLine))
            {
                var keyLen = fileLine.IndexOf('=');
                if (keyLen <= 0) continue;
                if (fileLine.Length <= keyLen) continue;

                var key = fileLine.Substring(0, keyLen);
                var value = fileLine.Substring(keyLen + 1, fileLine.Length);

                _settings.Add(key, value);
            }
        }

        public string GetValueForKey(string key)
        {
            return _settings[key];
        }
    }
}
