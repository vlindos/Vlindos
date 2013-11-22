namespace Vlindos.Common.Settings
{
    public interface ISettingsProvider
    {
        string GetValueForKey(string key);
    }
}