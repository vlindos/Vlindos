using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public interface IHttpRequestUnbinder : IHttpRequestUnbinder<int>
    {
    }

    public class HttpRequestUnbinder : IHttpRequestUnbinder
    {
        public bool TryToUnbind(IHttpRequest<int> httpRequest, IList<string> messages)
        {
            var numberString = httpRequest.QueryString["number"];
            if (string.IsNullOrWhiteSpace(numberString))
            {
                httpRequest.Request = 0;
                messages.Add("Query argument 'number' is not set.");
                return false;
            }
            int request;
            if (int.TryParse(numberString, out request) == false)
            {
                messages.Add(string.Format("Query argument 'number' is in unexpected number " +
                                           "format '{0}'. Must be like '1'.", numberString));
                return false;
            }
            httpRequest.Request = request;

            return true;
        }
    }
}