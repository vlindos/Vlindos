using System;

namespace Vlindos.Common.Logging
{
    public interface ILogger
    {
        bool DebugEnabled { get; }
        void Debug(string format, params object[] arguments);
        void Debug(Exception exception, string format, params object[] arguments);
        bool InfoEnabled { get; }
        void Info(string format, params object[] arguments);
        void Info(Exception exception, string format, params object[] arguments);
        bool WarnEnabled { get; }
        void Warn(string format, params object[] arguments);
        void Warn(Exception exception, string format, params object[] arguments);
        bool ErrorEnabled { get; }
        void Error(string format, params object[] arguments);
        void Error(Exception exception, string format, params object[] arguments);
        bool FatalEnabled { get; }
        void Fatal(string format, params object[] arguments);
        void Fatal(Exception exception, string format, params object[] arguments);
    }
}
