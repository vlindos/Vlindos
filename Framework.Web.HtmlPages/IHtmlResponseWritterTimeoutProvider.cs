using System;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlResponseWritterTimeoutProvider
    {
        TimeSpan Timeout { get; set; }
    }
}