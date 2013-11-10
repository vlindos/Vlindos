using System;
using System.Diagnostics;
using System.IO;
using Vlindos.Logging.Output;

namespace Vlindos.Logging
{
    public interface IInternalLogger
    {
        void Log(string format, params object[] arguments);
        void Log(Exception exception, string format, params object[] arguments);
    }

    public class InternalLogger : IInternalLogger
    {
        private readonly IMessageCreator _messageCreator;
        private readonly ICallingStackFrameGetter _callingStackFrameGetter;
        private readonly IMessageTextFormatter _messageTextFormatter;
        private readonly string _filePath;

        public InternalLogger(
            IMessageCreator messageCreator, 
            ICallingStackFrameGetter callingStackFrameGetter, 
            IMessageTextFormatter messageTextFormatter)
        {
            _messageCreator = messageCreator;
            _callingStackFrameGetter = callingStackFrameGetter;
            _messageTextFormatter = messageTextFormatter;
            _filePath = string.Format("{0}{1}{2}.{3}.log",
                Path.GetTempPath(),
                Environment.NewLine,
                AppDomain.CurrentDomain.FriendlyName,
                Process.GetCurrentProcess().Id);
        }

        public void Log(string format, params object[] arguments)
        {
            var message = _messageCreator.CreateMessage(
                null, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Debug, format, arguments);
            var text = _messageTextFormatter.GetMessageAsText(message);
            File.AppendAllText(_filePath, text);
        }

        public void Log(Exception exception, string format, params object[] arguments)
        {
            var message = _messageCreator.CreateMessage(
                null, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Debug, format, arguments);
            var text = _messageTextFormatter.GetMessageAsText(message);
            File.AppendAllText(_filePath, text);
        }
    }
}