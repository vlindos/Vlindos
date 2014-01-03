namespace Framework.Web.Tools
{
    public interface IApplicationRuntimeSettings
    {
        string BaseAddress { get; set; }
    }

    public class ApplicationRuntimeSettings : IApplicationRuntimeSettings
    {
        public string BaseAddress { get; set; }
    }
}