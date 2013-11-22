using System;

namespace Vlindos.Common.Settings
{
    public delegate bool ConvertFunc<T>(string input, out T output);
    public interface ISettingReader
    {
        string GetSetting(string settingName);
        string GetSetting(string settingName, Func<string> defaultFunc);
        bool ReadSetting<T>(string settingName, ConvertFunc<T> convertingFunc, out T result);
        bool ReadSetting<T>(string settingName, Func<T> defaultFunc, ConvertFunc<T> convertingFunc, out T result);
    }

    public interface ISettingReaderFactory
    {
        ISettingReader GetSettingReader(ISettingsProvider settingsProvider);
    }

    public class SettingReader : ISettingReader
    {
        private readonly ISettingsProvider _settingsProvider;

        public SettingReader(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public string GetSetting(string settingName)
        {
            return _settingsProvider.GetValueForKey(settingName);
        }

        public string GetSetting(string settingName, Func<string> defaultFunc)
        {
            var val = _settingsProvider.GetValueForKey(settingName);
            return val ?? defaultFunc();
        }

        public bool ReadSetting<T>(string settingName, Func<T> defaultFunc, ConvertFunc<T> convertingFunc, out T result)
        {
            var val = _settingsProvider.GetValueForKey(settingName);
            if (val == null)
            {
                result = defaultFunc();
                return false;
            }
            if (convertingFunc(val, out result) == false)
            {
                result = defaultFunc();
                return false;
            }
            return true;
        }
        public bool ReadSetting<T>(string settingName, ConvertFunc<T> convertingFunc, out T result)
        {
            var val = _settingsProvider.GetValueForKey(settingName);
            if (val == null)
            {
                result = default(T);
                return false;
            }
            if (convertingFunc(val, out result) == false)
            {
                result = default(T);
                return false;
            }
            return true;
        }
    }
}
