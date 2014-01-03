using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Tools;

namespace Framework.Web.DemoApp.Endpoints.Grep
{
    public interface IGrepRequestUnbinder : IHttpRequestUnbinder<GrepRequest>
    {
    }

    public class GrepRequestUnbinder : IGrepRequestUnbinder
    {
        private readonly IInputStreamStringReader _inputStreamStringReader;

        public GrepRequestUnbinder(IInputStreamStringReader inputStreamStringReader)
        {
            _inputStreamStringReader = inputStreamStringReader;
        }

        public bool TryToUnbind(HttpRequest httpRequest, List<string> messages, out GrepRequest request)
        {
            string s;
            if (_inputStreamStringReader.ReadStringFromInputStream(httpRequest.InputStream, messages, out s) == false)
            {
                request = null;
                return false;
            }

            request = new GrepRequest
            {
                InputString = s,
                FilterString = httpRequest.RoutesValues != null 
                    ? httpRequest.RoutesValues["for"]
                    : null
            };

            return true;
        }
    }
}