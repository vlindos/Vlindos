using System;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlPageWriteTimeoutProvider
    {
        TimeSpan Timeout { get; set; }
    }
}