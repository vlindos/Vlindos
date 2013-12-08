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
        public bool TryToUnbind(IHttpRequest httpRequest, out int request, IList<string> messages)
        {
            var numberString = httpRequest.QueryString["number"];
            if (string.IsNullOrWhiteSpace(numberString))
            {
                request = 0;
                messages.Add("Query argument 'number' is not set.");
                return false;
            }
            if (int.TryParse(numberString, out request) == false)
            {
                messages.Add(string.Format("Query argument 'number' is in unexpected number " +
                                           "format '{0}'. Must be like '1'.", numberString));
                return false;
            }

            return true;
        }
    }
}