using System.Collections.Specialized;
using Framework.Web.Models;

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