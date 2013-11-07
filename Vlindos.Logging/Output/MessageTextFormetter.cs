using System;
using System.Text;

namespace Vlindos.Logging.Output
{
    public interface IMessageTextFormatter
    {
        string GetMessageAsText(Message message);
    }

    public class MessageTextFormetter : IMessageTextFormatter
    {
        public string GetMessageAsText(Message message)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1} {2} {3}", 
                message.DateTimeOffset, message.Level, message.OriginNamespace, message.OriginMethod);
            sb.AppendLine();
            sb.AppendFormat(message.Format, message.FormatArguments);
            sb.AppendLine();
            if (message.ExceptionMessage == null || message.ExceptionStackTrace == null) return sb.ToString();
            sb.AppendLine(message.ExceptionMessage);
            sb.AppendLine(message.ExceptionStackTrace);
            return sb.ToString();
        }
    }
}