using System;
using Vlindos.Common.Configuration;
using Vlindos.Common.Logging;

namespace Vlindos.Logging
{
    public class Logger : ILogger
    {
        private readonly IMessageCreator _messageCreator;
        private readonly IMessagesDequeuer _messagesDequeuer;
        private readonly ICallingStackFrameGetter _callingStackFrameGetter;
        private readonly IContainer<Configuration.Configuration> _configurationContainer;

        public Logger(IMessageCreator messageCreator,
                      IMessagesDequeuer messagesDequeuer,
                      ICallingStackFrameGetter callingStackFrameGetter,
                      IContainer<Configuration.Configuration> configurationContainer)
        {
            _configurationContainer = configurationContainer;
            _messageCreator = messageCreator;
            _callingStackFrameGetter = callingStackFrameGetter;
            _messagesDequeuer = messagesDequeuer;
        }

        public bool DebugEnabled 
        { 
            get
            {
                return _configurationContainer.Configuration.Enabled &&
                       _configurationContainer.Configuration.MinimumLogLevel <= Level.Debug;
            } 
        }

        public void Debug(string format, params object[] arguments)
        {
            if (!DebugEnabled) return;
            var message = _messageCreator.CreateMessage(
                null, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Debug, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public void Debug(Exception exception, string format, params object[] arguments)
        {
            if (!DebugEnabled) return;
            var message = _messageCreator.CreateMessage(
                exception, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Debug, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public bool InfoEnabled
        {
            get
            {
                return _configurationContainer.Configuration.Enabled &&
                       _configurationContainer.Configuration.MinimumLogLevel <= Level.Info;
            }
        }

        public void Info(string format, params object[] arguments)
        {
            if (!InfoEnabled) return;
            var message = _messageCreator.CreateMessage(
                null, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Info, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public void Info(Exception exception, string format, params object[] arguments)
        {
            if (!InfoEnabled) return;
            var message = _messageCreator.CreateMessage(
                exception, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Info, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public bool WarnEnabled
        {
            get
            {
                return _configurationContainer.Configuration.Enabled &&
                       _configurationContainer.Configuration.MinimumLogLevel <= Level.Warn;
            }
        }
        public void Warn(string format, params object[] arguments)
        {
            if (!WarnEnabled) return;
            var message = _messageCreator.CreateMessage(
                null, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Warn, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public void Warn(Exception exception, string format, params object[] arguments)
        {
            if (!WarnEnabled) return;
            var message = _messageCreator.CreateMessage(
                exception, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Warn, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public bool ErrorEnabled
        {
            get
            {
                return _configurationContainer.Configuration.Enabled &&
                       _configurationContainer.Configuration.MinimumLogLevel <= Level.Error;
            }
        }
        public void Error(string format, params object[] arguments)
        {
            if (!ErrorEnabled) return;
            var message = _messageCreator.CreateMessage(
                null, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Error, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public void Error(Exception exception, string format, params object[] arguments)
        {
            if (!ErrorEnabled) return;
            var message = _messageCreator.CreateMessage(
                exception, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Error, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public bool FatalEnabled
        {
            get
            {
                return _configurationContainer.Configuration.Enabled &&
                       _configurationContainer.Configuration.MinimumLogLevel <= Level.Fatal;
            }
        }
        public void Fatal(string format, params object[] arguments)
        {
            if (!FatalEnabled) return;
            var message = _messageCreator.CreateMessage(
                null, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Fatal, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }

        public void Fatal(Exception exception, string format, params object[] arguments)
        {
            if (!FatalEnabled) return;
            var message = _messageCreator.CreateMessage(
                exception, _callingStackFrameGetter.GetCallingStackFrame(1), Level.Fatal, format, arguments);
            _messagesDequeuer.WriteMessage(message);
        }
    }
}
