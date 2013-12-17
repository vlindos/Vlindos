using System;
using System.Collections.Generic;
using Framework.Web.HtmlPages;

namespace Framework.Web.DemoApp.Endpoints.SimpleHtmlPage
{
    public class SimplePageViewData : IHtmlPageViewData
    {
        public IEnumerable<Uri> Urls { get; set; }
    }
}