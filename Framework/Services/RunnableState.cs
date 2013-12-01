namespace Vlindos.Common.Services
{
    public interface IRunnableStateFactory
    {
        IRunnableState GetRunnableState();
    }

    public interface IRunnableState
    {
        bool ShouldStop { get; set; }
        bool IsRunning { get; set; }
    }

    public class RunnableState : IRunnableState
    {
        public bool ShouldStop { get; set; }
        public bool IsRunning { get; set; }
    }
}