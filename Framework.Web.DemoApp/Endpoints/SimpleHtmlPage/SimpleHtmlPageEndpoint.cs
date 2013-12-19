using System;
using System.Collections.Generic;
using Framework.Web.HtmlPages;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.DemoApp.Endpoints.SimpleHtmlPage
{
    public class SimpleHtmlPageEndpoint : HtmlPageEndpointBase<object, IHtmlPageViewData>
    {
        public SimpleHtmlPageEndpoint(
            IHtmlActionEndpointBootstrapper bootstrapper,
            IGetHttpMethod getHttpMethod, ISimpleHtmlPage simpleHtmlPage)
        {
            bootstrapper.Bootstrap(
                this, 
                getHttpMethod, 
                "Simple/Html/Page",
                simpleHtmlPage,
                (httpRequest, httpResponse) =>
                {
                    var urlString = httpRequest.QueryString["url"];
                    Uri uri;
                    var viewData = new SimplePageViewData();
                    if (Uri.TryCreate(urlString, UriKind.RelativeOrAbsolute, out uri))
                    {
                        viewData.Urls = new List<Uri>{uri, uri, uri};
                    }
                    httpResponse.Response = viewData;
                });
        }
    }
}