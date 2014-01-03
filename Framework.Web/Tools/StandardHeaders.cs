using System.Collections.Generic;
using System.Net.Mime;

namespace Framework.Web.Tools
{
    public class StandardHeaders
    {
        public string HttpUsername { get; set; }

        public string HttpPassword { get; set; }

        public string UserAgent { get; set; }

        public Dictionary<string, object> Session { get; set; }
 
        public ContentType ContentType { get; set; }
    }
}