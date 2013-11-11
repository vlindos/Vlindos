using System;
using System.Threading;
using Castle.Core.Logging;

namespace Users.Common.Services
{
    public interface IBackgroundTaskStarter
    {
        IRunnableState Start(Func<IRunnable> runnableFactory);
    }

    public class BackgroundTaskStarter : IBackgroundTaskStarter
    {
        private readonly IExtendedLogger _logger;
        private readonly IRunnableStateFactory _runnableStateFactory;
        private Thread _thread;
        private readonly int _pauseInMilisecs;
        private readonly int _failurePauseInMilisecs;

        public BackgroundTaskStarter(IExtendedLogger logger, IRunnableStateFactory runnableStateFactory, 
            int pauseInMilisecs = 0, int failurePauseInMilisecs = 1000)
        {
            _logger = logger;
            _runnableStateFactory = runnableStateFactory;
            _pauseInMilisecs = pauseInMilisecs;
            _failurePauseInMilisecs = failurePauseInMilisecs;
        }

        public IRunnableState Start(Func<IRunnable> runnableFactory)
        {
            var runnableState = _runnableStateFactory.GetRunnableState();
            runnableState.ShouldStop = false;
         
            _thread = new Thread(() =>
                {
                    IRunnable runnable = null;
                    while (!runnableState.ShouldStop) 
                    {
                        // NOTE: non throwing exceptions try/catch blocks are cheap, managing an watch dog is expensive
                        try
                        {
                            runnable = runnableFactory.Invoke();
                            runnableState.IsRunning = true;
                            _logger.InfoFormat("Launched '{0}' in the background.", runnable.Identifier);
                            while (!runnableState.ShouldStop)
                            {
                                runnable.Run();
                                Thread.Sleep(_pauseInMilisecs);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.ErrorFormat(ex, "Runnable did throw an exception. It will be restarted after a '{0}'.", _failurePauseInMilisecs);
                            Thread.Sleep(_failurePauseInMilisecs); 
                            if (runnable != null)
                            {
                                runnable.Dispose();
                            }
                            //Thread.Sleep(_failurePauseInMilisecs); 
                        }
                    }
                    runnableState.IsRunning = false;
                    if (runnable != null)
                    {
                        try
                        {
                            runnable.Dispose();
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Exception thrown while disposing runnable.", ex);
                        }
                        _logger.InfoFormat("Stopped '{0}' from background.", runnable.Identifier);
                    }
                });

            _thread.Start();
            return runnableState;
        }
    }
}