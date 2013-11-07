using System;
using System.Diagnostics;

namespace Vlindos.Logging
{
    public interface IMessageCreator
    {
        Message CreateMessage(
            Exception exception, StackFrame stackFrame, Level level, string format, params object[] args);
    }

    public class MessageCreator : IMessageCreator
    {
        private readonly IMessageModifier[] _messageModifiers;

        public MessageCreator(IMessageModifier[] messageModifiers)
        {
            _messageModifiers = messageModifiers;
        }

        public Message CreateMessage(
            Exception exception, StackFrame stackFrame, Level level, string format, params object[] args)
        {
            var message = new Message
            {
                DateTimeOffset = DateTimeOffset.Now,
                Format = format,
                FormatArguments = args,
                Level = level
            };

            if (stackFrame != null)
            {
                message.FileNumber = (uint)stackFrame.GetFileLineNumber();
                message.FileName = stackFrame.GetFileName();
                var method = stackFrame.GetMethod();
                message.OriginMethod = method.Name;
                var declaringType = stackFrame.GetMethod().DeclaringType;
                if (declaringType != null)
                {
                    message.OriginNamespace = declaringType.FullName;
                }
            }

            if (exception != null)
            {
                message.ExceptionMessage = exception.Message;
                message.ExceptionStackTrace = exception.StackTrace;
            }

            foreach (var messageModifier in _messageModifiers)
            {
                messageModifier.ModifyMessage(message);
            }
            
            return message;
        }
    }
}
