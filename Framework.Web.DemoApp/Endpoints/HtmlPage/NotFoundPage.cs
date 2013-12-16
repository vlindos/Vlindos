using System;
using Framework.Web.HtmlPages;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.DemoApp.Endpoints.HtmlPage
{
    public class NotFoundViewData : IHtmlPageViewData
    {
        public Uri NotFoundUri { get; set; }
    }

    public class NotFoundEndpoint : HtmlActionEndpointBase<object, IHtmlPageViewData>
    {
        public NotFoundEndpoint(
            IHtmlActionEndpointBootstrapper bootstrapper,
            IGetHttpMethod getHttpMethod)
        {
            bootstrapper.Bootstrap<NotFoundViewData>(this, getHttpMethod, "NotFound", (httpRequest, httpResponse) =>
                {
                    var urlString = httpRequest.QueryString["url"];
                    Uri uri;
                    var viewData = new NotFoundViewData();
                    if (Uri.TryCreate(urlString, UriKind.RelativeOrAbsolute, out uri))
                    {
                        viewData.NotFoundUri = uri;
                    }
                    httpResponse.Response = viewData;
                });
        }
    }
}