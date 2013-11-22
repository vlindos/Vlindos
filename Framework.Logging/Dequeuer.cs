using System;
using System.Collections.Generic;
using System.Timers;
using Vlindos.Common.Configuration;
using Vlindos.Logging.Configuration;

namespace Vlindos.Logging
{
    public interface IMessagesDequeuer
    {
        void Start();
        void Stop();
        void WriteMessage(Message message);
    }
                                                                                      
    public class MessagesDequeuer : IMessagesDequeuer
    {
        private readonly IContainer<Configuration.Configuration> _configurationContainer;

        private readonly List<Tuple<Timer, OutputPipe, Object>> _dequeuers;

        public MessagesDequeuer(IContainer<Configuration.Configuration> configurationContainer)
        {
            _configurationContainer = configurationContainer;
            _dequeuers = new List<Tuple<Timer, OutputPipe, object>>();
        }

        public void Start()
        {
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                if (outputPipe.BufferMaximumKeepTime == TimeSpan.Zero) continue;
                var timer = new Timer
                {
                    Enabled = true,
                    AutoReset = true, 
                    Interval = outputPipe.BufferMaximumKeepTime.TotalMilliseconds,

                };
                var tuple = new Tuple<Timer, OutputPipe, object>(timer, outputPipe, new object());
                timer.Elapsed += (sender, args) => Dequeue(tuple);
                timer.Start();
                _dequeuers.Add(tuple);
            }
        }

        public void WriteMessage(Message message)
        {
            foreach (var outputPipe in _configurationContainer.Configuration.OutputPipes)
            {
                if (outputPipe.BufferMaximumKeepTime == TimeSpan.Zero)
                {
                    outputPipe.OutputEngine.SaveMessages(message);
                }
                else
                {
                    outputPipe.Queue.AddMessage(message);
                }
            }
        }

        private static void Dequeue(Tuple<Timer, OutputPipe, Object> tuple)
        {
            lock (tuple.Item3)
            {
                var messages = tuple.Item2.Queue.GetMessages();
                if (messages.Length == 0) return;
                var submittedMessages = tuple.Item2.OutputEngine.SaveMessages(messages);
                tuple.Item2.Queue.DequeueMessages(submittedMessages);
            }
        }

        public void Stop()
        {
            foreach (var dequeuer in _dequeuers)
            {
                lock (dequeuer.Item3)
                {
                    dequeuer.Item1.Stop();
                }
            }
        }
    }
}
