using System;
using System.Net.Mime;
using System.Text;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service.Rest.Models;
using Framework.Web.Service.Rest.Tools;

namespace Framework.Web.DemoApp.Endpoints.Sample
{
    public class SampleRestEndpoint : RestEndpointBase<string, EchoResponse>
    {
        public SampleRestEndpoint(IRestEndpointBootstrapper bootstrapper, IGetHttpMethod getHttpMethod)
        {
            bootstrapper.Bootstrap(this, getHttpMethod, "echo/{0}",
                unbind: (httpRequest, messages) =>
                {
                    httpRequest.Request = httpRequest.QueryString["arg"];
                    return true;
                },
                perform: request => new EchoResponse { String = request },
                responseWritter: (httpRequest, httpResponse) =>
                {
                    var encoding = new UTF8Encoding();
                    var bytes = encoding.GetBytes(httpResponse.Response.String);
                    httpResponse.ContentType = new ContentType
                    {
                        CharSet = encoding.EncodingName, 
                        MediaType = "text/plain"
                    };
                    httpResponse.OutputStream.Write(bytes, new TimeSpan(0, 0, 0, 1));
                });
        }
    }
}