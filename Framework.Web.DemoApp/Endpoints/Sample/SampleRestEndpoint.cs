using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.DemoApp.Filters;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service.Rest.Models;
using Framework.Web.Service.Rest.Tools;

namespace Framework.Web.DemoApp.Endpoints.Sample
{
    public class SampleRestEndpoint : RestEndpointBase<string, EchoResponse>
    {
        public SampleRestEndpoint(IRestEndpointBootstrapper bootstrapper, IGetHttpMethod getHttpMethod, IAuthenticationFilter authenticationFilter)
        {
            bootstrapper.Bootstrap(this, getHttpMethod, "echo/{0}",
                unbind: (httpRequest, messages) =>
                {
                    httpRequest.Request = httpRequest.QueryString["arg"];
                    return true;
                },
                perform: (httpRequest, httpResponse) => httpResponse.Response = new EchoResponse { String = httpRequest.Request },
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
                }, filters: new List<IHttpEndpointFilter>{authenticationFilter});
        }
    }
}