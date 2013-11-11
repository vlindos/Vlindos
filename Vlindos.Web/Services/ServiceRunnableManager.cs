namespace Vlindos.Web.Services
{
    public interface IServiceRunnableManager
    {
        uint Copies { get; }
        IRunnable GetRunnable(string copyIdentity);
    }
}