using System.Collections.Generic;

namespace Vlindos.Logging
{
    public interface IQueueFactory
    {
        IQueue GetQueue();
    }

    public interface IQueue
    {
        void AddMessage(Message message);
        Message[] GetMessages();
        void DequeueMessages(Message[] messages);
    }

    public class Queue : IQueue
    {
        private readonly List<Message> _messages;

        public Queue()
        {
            _messages = new List<Message>();
        }

        public void AddMessage(Message message)
        {
            lock (_messages)
            {
                _messages.Add(message);
            }
        }

        public Message[] GetMessages()
        {
            Message[] messages;
            lock (_messages)
            {
                messages = _messages.ToArray();
            }
            return messages;
        }

        public void DequeueMessages(Message[] messages)
        {
            lock (_messages)
            {
                foreach (var message in messages)
                {
                    _messages.Remove(message);
                }
            }
        }
    }
}
