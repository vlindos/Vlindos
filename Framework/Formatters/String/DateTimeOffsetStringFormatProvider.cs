namespace Vlindos.Common.Formatters.String
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
