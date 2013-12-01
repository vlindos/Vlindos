namespace Vlindos.Common.Services
{
    public interface IServiceRunnableManager
    {
        uint Copies { get; }
        IRunnable GetRunnable(string copyIdentity);
    }
}