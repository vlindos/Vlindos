namespace Users.Common.Services
{
    public interface ServiceRunnableManager
    {
        uint Copies { get; }
        IRunnable GetRunnable(string copyIdentity);
    }
}