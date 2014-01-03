using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.DemoApp.Endpoints.Grep
{

    public interface IGrepRequestValidator : IRequestValidator<GrepRequest>
    {
    }

    public class GrepRequestValidator : IGrepRequestValidator
    {
        public bool Validate(GrepRequest request, List<string> messages)
        {
            if (request.FilterString == null)
            {
                messages.Add("'for' string parameter is not specified " +
                             "but required to filter the posted string against string.");

                return false;
            }

            return true;
        }
    }
}