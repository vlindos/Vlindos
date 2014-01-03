using System.Collections.Specialized;

namespace Framework.Web.Tools
{
    public interface IStandardHeadersReader
    {
        StandardHeaders ReadStandardHeaders(NameValueCollection headers);
    }

    public class StandardHeadersReader : IStandardHeadersReader
    {
        public StandardHeaders ReadStandardHeaders(NameValueCollection headers)
        {
            return new StandardHeaders
            {
            };
        }
    }
}