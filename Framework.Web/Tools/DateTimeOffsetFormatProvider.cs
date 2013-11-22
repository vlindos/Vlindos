using Vlindos.Web.Models;

namespace Vlindos.Web.Tools
{
    public interface IDateTimeOffsetFormatProvider : IFormatProvider
    {
    }

    public class DateTimeOffsetFormatProvider : IDateTimeOffsetFormatProvider
    {
        private readonly string _format;
        public DateTimeOffsetFormatProvider()
        {
            _format = "yyyy-MM-ddTHH:mm:ss.FFzzz";
        }
        public string Format {
            get { return _format; }
        }
    }
}
